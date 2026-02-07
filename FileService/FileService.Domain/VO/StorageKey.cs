using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SharedKernel.Exseption;
using System.Net.Sockets;

namespace FileService.Domain.VO
{
    public record StorageKey
    {
        private StorageKey() { }

        private StorageKey(string valuekey, string? prefix, string bucket)
        {
            ValueKey = valuekey;
            Prefix = prefix;
            Bucket = bucket;
            ValuePreKey = string.IsNullOrEmpty(prefix) ? ValueKey : $"{Prefix}/{ValueKey}";
            FullPath = $"{Bucket}/{ValuePreKey}";
        }

        public string ValueKey { get; }

        public string? Prefix { get; }

        public string Bucket { get; }

        public string ValuePreKey { get; }

        public string FullPath { get; }

        public static Result<StorageKey, Error> Create(string valuekey, string? prefix, string bucket)
        {
            if (string.IsNullOrWhiteSpace(valuekey))
                return GeneralErrors.ValueNotValid("key");

            valuekey = NormalazSegment(valuekey).Value;

            if (string.IsNullOrWhiteSpace(bucket))
                return GeneralErrors.ValueNotValid("bucket");

            bucket = NormalazSegment(bucket).Value;

            return new StorageKey(valuekey, prefix, bucket);
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

            var newKey = $"{ValueKey}/{normalSegment}";

            return StorageKey.Create(newKey, Prefix, Bucket);
        }
    }
}
