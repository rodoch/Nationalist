# Nationalist

***A chairde Gael: muna spéis leat ach liosta cuimsitheach tíortha i nGaeilge agus/nó i mBéarla a fháil i bhformáidí éagsúla (cuid acu atá meaisín-inléite), moltar cuairt a thabhairt ar an fhillteán `output` thuas***

## Background

On the surface, the idea of programmatically generating a list of all the countries in the world seems like it should be a simple task and, generally speaking, computer programmers will draw on the authoritative data provided by the Unicode [Common Locale Data Repository](http://cldr.unicode.org/) (CLDR) to accomplish this. Furthermore, the well-known [GeoNames](http://www.geonames.org/) geographical database has a useful `CountryInfo` table. Despite this, I've repeatedly encountered a number of issues:

- If all we want is a list of countries, then the CLDR data contain a lot of noise. There is no concept of countries within the CLDR, only territories and subdivisions. The territories data contains not only countries but continents, oceans and entities like the United Nations.
- Similarly, while GeoNames does define its own country list with authoritiative English-language names there are often multiple localised forms of these names, sometimes making it difficult to find authoritative country names in various languages or to understand the relationships between the alternate forms.
- When generating a list of countries for public-facing applications it is often desirable not to display multiple forms of each country name, e.g. the United States of America *and* USA; or, the Democratic Republic of Timor-Leste *and* East Timor. In such cases, the original list may be reduced to only the best locally-understood names within a particular locale. This process, and the decisions that go into it, is often opaque and undocumented.
- The concept of a 'country' is itself highly problematic, and perhaps this is a clue as to why the CLDR avoids using the term. Both the CLDR territories data and the GeoNames CountryInfo data appear to conform to the [ISO 3166-1](https://www.iso.org/iso-3166-country-codes.html) standard. This standard may be said to reflect a list of the fully-sovereign independent policital entities in the world, however this list may not map easily on to many people's conception of countries in the world. For example, many people living in the United Kingdom conceive of England, Northern Ireland, Scotland, and Wales as four separate nations (the case of Northern Ireland is further complicated by its policital and historical circumstances). Each of these regions, for example, fields its own national sports team in most sports. However, they do not feature in ISO 3166-1; rather, they are recognised under the ISO 3166-2 standard as subdivisions of the United Kingdom. This type of situation is found in many regions around the world. For many administrative purposes this type of situation does not present a major challenge: if generating a list of countries for an online ordering system, for instance, postal issues are typically handled by the parent political entity and the particular subdivision might be specified in another field. However, situations do arise where we need to generate a list of countries that are more closely aligned with popular conceptions.

## Solution

**Nationalist** provides a transparent build process for generating curated country lists and outputting them in various human- and machine-readable formats. Furthermore, these lists are annotated with both ISO-3166 and GeoNames identifiers. The output formats are:

- C# static dictionaries (code-generated using the Roslyn compiler)
- CSV
- JSON
- TSV

Customise the list-curation process by adding your own code to the `ModifyList()` method of the **Modifier** class.

## How it works

**Nationalist** takes the CLDR as the authoritative source for country names. Noise is filtered from the CLDR territories list by comparing it with the countries data held by GeoNames and reducing the list to a subset of territories common to both data stores. The resulting list may then, optionally, be processed by the Modifier class to produce the curated list. The final list is then piped to several generator services to create the final output.
