﻿using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.IO;

namespace Knapcode.NCsvPerf.CsvReadable.TestCases
{
    public class PackageAssetsSuite
    {
        private readonly byte[] _bytes;
        private readonly bool _saveResult;

        public PackageAssetsSuite() : this(saveResult: false)
        {
        }

        public PackageAssetsSuite(bool saveResult)
        {
            _bytes = File.ReadAllBytes(Path.Combine("TestData", "PackageAssets.csv"));
            _saveResult = saveResult;
        }

        public List<PackageAsset> LatestResult { get; private set; }

        [Benchmark]
        public void CsvHelperCsvReader()
        {
            Execute(new CsvHelperCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void CsvTextFieldParserCsvReader()
        {
            Execute(new CsvTextFieldParserCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void FastCsvParserCsvReader()
        {
            Execute(new FastCsvParserCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void HomeGrownCsvReader()
        {
            Execute(new HomeGrownCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void LumenWorksCsvReader()
        {
            Execute(new LumenWorksCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void MgholamFastCsvReader()
        {
            Execute(new MgholamFastCsvReader());
        }

        [Benchmark]
        public void NRecoCsvReader()
        {
            Execute(new NRecoCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void ServiceStackTextCsvReader()
        {
            Execute(new ServiceStackTextCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void SteveHansenCsvReader()
        {
            Execute(new ReallySimpleCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void StringSplitCsvReader()
        {
            Execute(new StringSplitCsvReader(ActivationMethod.ILEmit));
        }

        [Benchmark]
        public void TinyCsvReader()
        {
            Execute(new TinyCsvReader(ActivationMethod.ILEmit));
        }

        private void Execute(ICsvReader reader)
        {
            using (var memoryStream = new MemoryStream(_bytes, writable: false))
            {
                var result = reader.GetRecords<PackageAsset>(memoryStream);
                if (_saveResult)
                {
                    LatestResult = result;
                }
            }
        }
    }
}
