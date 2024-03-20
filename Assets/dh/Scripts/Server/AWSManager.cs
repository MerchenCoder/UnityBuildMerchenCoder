using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;





public class AWSManager : MonoBehaviour
{
    string CognitoIdentityPoolId = "ap-northeast-2:bc5cf91e-7083-4df8-aaaf-9e0cc09e9e5d";
    public RegionEndpoint Endpoint = RegionEndpoint.APNortheast2;
    private AmazonS3Client s3Client;


    //s3 버킷 이름, 로컬 저장 경로
    string S3BucketName = "merhencoderbucket";
    string LocalDownloadPath = Path.Combine(Application.dataPath, "Data/"); // 로컬 다운로드 경로를 지정하세요

    string localVersionFilePath;


    //파일 이름과 버전 저장하는 딕셔너리
    public Dictionary<string, int> localFileVersions = new Dictionary<string, int>();


    void Start()
    {
        localVersionFilePath = Path.Combine(Application.dataPath, "Data/version.json");
        Debug.Log("Local Version File Path: " + LocalDownloadPath);


        //Cognito credential provider 초기화
        CognitoAWSCredentials credentials = new CognitoAWSCredentials(CognitoIdentityPoolId, Endpoint);
        // AWS Credentials를 설정합니다. 여기서는 DefaultCredentials를 사용합니다.
        s3Client = new AmazonS3Client(credentials, RegionEndpoint.APNortheast2);


        //로컬에 있는 파일의 버전 추적
        TrackLocalFileVersions();

        // 버킷에서 파일을 다운로드
        GetObjects();
    }


    //파일 추적 메소드
    private void TrackLocalFileVersions()
    {
        if (File.Exists(localVersionFilePath))
        {
            string json = File.ReadAllText(localVersionFilePath);
            localFileVersions = JsonUtility.FromJson<Dictionary<string, int>>(json);
        }


    }
    private void SaveLocalFileVersions()
    {
        string json = JsonUtility.ToJson(localFileVersions);
        File.WriteAllText(localVersionFilePath, json);
        Debug.Log("save함");
    }


    private bool TryExtractVersion(string fileName, out string fileNameWithoutVersion, out int version)
    {
        fileNameWithoutVersion = fileName;
        version = 0;
        // 파일 이름을 "_"로 분리하여 버전을 추출합니다.
        string[] parts = fileName.Split('_');
        if (parts.Length >= 2)
        {
            // 마지막 부분이 버전 번호인지 확인하고 추출합니다.
            string versionString = parts[parts.Length - 1];
            if (int.TryParse(versionString.Substring(1), out version))
            {
                // 버전을 제외한 파일 이름을 설정합니다.
                fileNameWithoutVersion = string.Join("_", parts.Take(parts.Length - 1));
                return true;
            }
        }
        return false;
    }

    public async void GetObjects()
    {
        Debug.Log("GetObjects 시작");
        ListObjectsV2Request request = new ListObjectsV2Request
        {
            BucketName = S3BucketName,
            Delimiter = "/"
        };
        try
        {
            ListObjectsV2Response response;
            string[] prefixes = { "", "MissionInfo/", "TestCase/" };
            foreach (string prefix in prefixes)
            {
                request.Prefix = prefix;

                do
                {
                    response = await s3Client.ListObjectsV2Async(request);
                    foreach (S3Object s3Object in response.S3Objects)
                    {
                        await FileCheckAndDownload(prefix, s3Object.Key);
                    }
                    request.ContinuationToken = response.NextContinuationToken;
                } while (response.IsTruncated);
                SaveLocalFileVersions();
            }

        }
        catch (AmazonS3Exception e)
        {
            Debug.LogError($"오류 발생: {e.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"예외 발생: {ex.Message}");
        }
    }


    private async Task<bool> FileCheckAndDownload(string prefix, string key)
    {
        string fileName = Path.GetFileNameWithoutExtension(key);
        int version;
        string fileNameWithoutVersion;

        if (!TryExtractVersion(fileName, out fileNameWithoutVersion, out version))
            return false;

        if (!localFileVersions.ContainsKey(fileNameWithoutVersion) || version > localFileVersions[fileNameWithoutVersion])
        {
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = S3BucketName,
                    Key = key

                };

                GetObjectResponse response = await s3Client.GetObjectAsync(request);

                using (Stream responseStream = response.ResponseStream)
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string data = reader.ReadToEnd();
                        string extension = Path.GetExtension(key);
                        string filePath;
                        if (prefix != "")
                        {
                            Debug.Log(prefix);
                            filePath = Path.Combine(LocalDownloadPath, prefix, fileNameWithoutVersion + extension); // 버전이 제거된 이름으로 파일 저장
                        }
                        else
                        {
                            Debug.Log("prefix is null");
                            filePath = Path.Combine(LocalDownloadPath, fileNameWithoutVersion + extension); // 버전이 제거된 이름으로 파일 저장
                        }

                        Debug.Log(filePath);
                        string directoryPath = Path.GetDirectoryName(filePath);
                        Directory.CreateDirectory(directoryPath);
                        File.WriteAllText(filePath, data);

                        localFileVersions[fileNameWithoutVersion] = version;
                        Debug.Log($"다운로드 완료: {key}");
                        Debug.Log($"다운로드된 파일 경로: {filePath}");
                    }
                }
                SaveLocalFileVersions();
                return true;

            }
            catch (AmazonS3Exception e)
            {
                Debug.LogError($"파일 다운로드 중 오류 발생: {e.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.LogError($"파일 다운로드 중 예외 발생: {ex.Message}");
                return false;
            }
        }
        else
        {
            Debug.Log($"이미 존재하는 파일입니다. 버전이 동일합니다.: {fileNameWithoutVersion}");
            return false;
        }
    }

}
