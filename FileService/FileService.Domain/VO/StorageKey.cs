using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SharedKernel.Exseption;
using System.Net.Sockets;

namespace FileService.Domain.VO
{
    public record StorageKey
    {
        private StorageKey() { }

        private StorageKey(string key, string? prefix, string bucket)
        {
            Key = key;
            Prefix = prefix;
            Bucket = bucket;
            Value = string.IsNullOrEmpty(prefix) ? Key : $"{Prefix}/{Key}";
            FullPath = $"{Bucket}/{Value}";
        }

        public string Key { get; }

        public string? Prefix { get; }

        public string Bucket { get; }

        public string Value { get; }

        public string FullPath { get; }

        public static Result<StorageKey, Error> Create(string key, string? prefix, string bucket)
        {
            if (string.IsNullOrWhiteSpace(key))
                return GeneralErrors.ValueNotValid("key");

            key = NormalazSegment(key).Value;

            if (string.IsNullOrWhiteSpace(bucket))
                return GeneralErrors.ValueNotValid("bucket");

            bucket = NormalazSegment(bucket).Value;

            return new StorageKey(key, prefix, bucket);
        }

        private static Result<string, Error> NormalazSegment(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                return GeneralErrors.ValueNotValid("value");

            var normalSegment = value.Trim();

            normalSegment = value.Contains("\\") ? normalSegment.Replace("\\", "/") : value;

            return normalSegment;
        }

        public Result<StorageKey, Error> AppendSegment(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
                return GeneralErrors.ValueNotValid("segment");
            var normalSegment = NormalazSegment(segment);

            var newKey = $"{Key}/{normalSegment}";

            return StorageKey.Create(newKey, Prefix, Bucket);
        }
    }
}
