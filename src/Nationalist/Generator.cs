using System.Linq;
using Nationalist.Core;

namespace Nationalist
{
    public class Generator
    {
        private readonly IReducer _reducer;
        private readonly CSharpGeneratorService _cSharpGeneratorService;
        private readonly CsvGeneratorService _csvGeneratorService;
        private readonly JsonGeneratorService _jsonGeneratorService;
        private readonly TsvGeneratorService _tsvGeneratorService;

        public Generator(
            IReducer reducer,
            CSharpGeneratorService cSharpGeneratorService,
            CsvGeneratorService csvGeneratorService,
            JsonGeneratorService jsonGeneratorService,
            TsvGeneratorService tsvGeneratorService)
        {
            _reducer = reducer;
            _cSharpGeneratorService = cSharpGeneratorService;
            _csvGeneratorService = csvGeneratorService;
            _jsonGeneratorService = jsonGeneratorService;
            _tsvGeneratorService = tsvGeneratorService;
        }

        public void GenerateList(string locale, IModifier modifier = default(IModifier))
        {
            var countries = _reducer.GenerateList(locale);

            if (modifier != default(IModifier))
            {
                var modifiedCountries = modifier.ModifyList(countries, locale);
                countries = modifiedCountries;
            }

            countries = countries.OrderBy(c => c.Code).ToList();

            _cSharpGeneratorService.WriteCSharp(countries, locale);
            _csvGeneratorService.WriteCsv(countries, locale);
            _jsonGeneratorService.WriteJson(countries, locale);
            _tsvGeneratorService.WriteTsv(countries, locale);
        }
    }
}