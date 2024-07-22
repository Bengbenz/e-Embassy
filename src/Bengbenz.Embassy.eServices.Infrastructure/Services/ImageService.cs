// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

namespace Bengbenz.Embassy.eServices.Infrastructure.Services;

// using Amazon.S3;
// using Amazon.S3.Model;
// using Amazon.CloudFront;
// using System;

public class ImageService
{
  // private readonly AmazonS3Client _s3Client;
  // private readonly string _bucketName = "your-bucket-name";
  // private readonly string _cloudFrontDistributionId = "your-distribution-id";
  // private readonly string _cloudFrontDomainName = "your-distribution-name.cloudfront.net";
  //
  // public ImageService(AmazonS3Client s3Client)
  // {
  //   _s3Client = s3Client;
  // }
  //
  // public async Task<string> UploadImageAsync(Stream imageData, string fileName)
  // {
  //   var putRequest = new PutObjectRequest
  //   {
  //     BucketName = _bucketName,
  //     Key = fileName,
  //     InputStream = imageData,
  //     CannedACL = S3CannedACL.Private // Ensure the image is not publicly accessible
  //   };
  //
  //   await _s3Client.PutObjectAsync(putRequest);
  //
  //   return GenerateSignedUrl(fileName);
  // }
  //
  // private string GenerateSignedUrl(string fileName)
  // {
  //   // Assuming you have a method to generate a signed URL for CloudFront
  //   // This is a placeholder for the actual implementation, which varies based on the SDK and specific use case
  //   return $"https://{_cloudFrontDomainName}/{fileName}?{GenerateSignature()}";
  // }
  //
  // private string GenerateSignature()
  // {
  //   // Placeholder for signature generation logic
  //   // This typically involves using the AWS SDK to create a signed URL that grants temporary access to the private file
  //   return "signature";
  // }
}
