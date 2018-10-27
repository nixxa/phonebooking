using System;
using System.IO;
using LiteDB;
using Microsoft.Extensions.Options;
using Models;

namespace Kernel
{
    public class DataProviderFactory : IDataProviderFactory
    {
        private readonly DatabaseOptions _options;

        public DataProviderFactory(IOptions<DatabaseOptions> options)
        {
            if (options.Value == null) throw new ArgumentNullException(nameof(options));
            _options = options.Value;
            Configure();
        }

        private void Configure()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<Phone>().Id(x => x.Id);
        }

        public LiteDatabase Create()
        {
            bool exist = File.Exists(_options.Path);
            var result = new LiteDatabase(_options.Path);
            if (!exist)
            {
            }
            return result;
        }
    }
}