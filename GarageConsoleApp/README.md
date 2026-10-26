# Garage 2.0

Ett konsolprogram i C# där man skapar ett garage, parkerar fordon, söker bland dem och ser hur mycket plats som används. Det här är min version 2 av garageuppgiften. Jag vill gärna ha er syn på den innan jag bygger vidare.

## Kort om garaget

- Man väljer själv namn och antal parkeringsplatser (1–100), och om garaget ska börja tomt eller fyllas med exempelfordon.
- Det finns fem fordonstyper: bil, motorcykel, buss, båt och flygplan. Alla har registreringsnummer, modell, färg och antal hjul. Varje typ har dessutom en egen egenskap: bränsle (bil), växellåda (motorcykel), antal säten (buss), längd (båt) och antal motorer (flygplan).
- Fordon tar olika mycket plats. En parkeringsplats är uppdelad i 3 enheter. En motorcykel tar en tredjedels plats, en bil en plats, en buss och en båt tre platser och ett flygplan åtta. Programmet visar platserna med bråk, t.ex. `1 1/3`.
- I garagemenyn kan man hantera parkeringen, lista parkerade fordon, se hur många av varje typ som står i garaget och söka på registreringsnummer eller på en kombination av typ, färg, antal hjul och modell.

## Så startar du programmet

Du behöver .NET 10 SDK. Kör från repots rot:

```
dotnet run --project GarageConsoleApp
```

Navigera med piltangenterna och Enter. I *Manage parking* kryssar man med mellanslag, tillämpar med Enter och avbryter med Esc.

## Så är koden organiserad

```
GarageConsoleApp/
├── Program.cs                  startar programmet
├── Garage.cs                   garaget och dess regler
├── ParkingUnits.cs             räknar och visar parkeringsplatser
├── ParkResult.cs               svaret när man parkerar
├── VehicleRegistry.cs          exempelfordonen
├── DuplicateRegistrationNumberException.cs
├── Models/                     fordonen (Vehicle och subklasserna), FuelType, TransmissionType, VehicleFilter
└── UI/                         allt som visas och frågas i konsolen
    ├── ConsoleUI.cs            gemensamma hjälpmetoder
    ├── Table.cs, VehicleTable.cs
    ├── Menus/                  menyerna och MenuRunner
    ├── Forms/                  formulär som ställer frågor
    └── Views/                  vyer som bara visar information
```

## Klasser och ansvar

**Garage och fordon**

| Klass | Ansvar |
|---|---|
| `Garage` | Håller en privat `List<Vehicle>` med parkerade fordon och kapaciteten. Kan parkera, ta bort, hitta, söka och räkna fordon per typ, och vet hur mycket plats som är använd och ledig. |
| `Vehicle` (abstrakt) | Det som alla fordon har gemensamt: registreringsnummer, modell, färg, antal hjul och storlek. |
| `Car`, `Motorcycle`, `Bus`, `Boat`, `Airplane` | Varje typ har en egen standardstorlek och minst en egen egenskap. |
| `VehicleFilter` | En sökning med valfria kriterier (typ, färg, hjul, modell). Kan avgöra om ett fordon matchar. |
| `ParkingUnits` | Räknar mellan parkeringsplatser och enheter (1 plats = 3 enheter) och formaterar dem för visning. |
| `ParkResult` | Svaret när man försöker parkera: parkerad, inte tillräckligt med plats eller redan parkerad. |
| `VehicleRegistry` | Skapar de 19 exempelfordonen och kontrollerar att inget registreringsnummer förekommer två gånger. |
| `DuplicateRegistrationNumberException` | Kastas om exempelfordonen skulle ha samma registreringsnummer. |

**Gränssnittet** (mappen `UI`)

| Del | Ansvar |
|---|---|
| `ConsoleUI` | Gemensamma hjälpmetoder för rubriker, texter, val med piltangenter och inläsning av text och tal. |
| `Menus` | `StartMenu`, `GarageMenu` och `SearchMenu`, samt `MenuRunner` som visar en meny och kör det som väljs. |
| `Forms` | Formulär som ställer frågor: skapa garage, hantera parkering, hitta på registreringsnummer och söka på egenskaper. |
| `Views` | Vyer som bara visar information: listan över parkerade fordon och antal per typ. |
| `Table`, `VehicleTable` | Skriver ut fordon i en tabell med jämna kolumner. |

## Kända begränsningar

- Allt ligger bara i minnet och försvinner när programmet stängs.
- Man kan inte skapa egna fordon, bara använda exempelfordonen.
- Ett garage går inte att ändra efter att det skapats, och programmet hanterar bara ett garage.

## Vad jag gärna vill ha feedback på

- **Arkitekturen.** Jag har delat upp koden i `Models` för fordonen, `UI` med `Menus`, `Forms` och `Views` för det som visas i konsolen, och några filer i roten (`Garage`, `ParkingUnits` m.fl.). Är det begripligt när ni läser koden, och hur hade ni kunnat förbättra uppdelningen?
- **Fördelningen av logik.** *Manage parking* räknar ut hur mycket plats de ikryssade fordonen behöver och bestämmer i vilken ordning fordon tas bort och parkeras. Hör den logiken hemma i formuläret, eller borde `Garage` sköta det?
- **Garaget och fordonslistan.** `StartMenu` håller garaget och fordonslistan i två separata variabler som skickas runt till flera ställen. Finns det ett bättre sätt?
- **Platser med olika storlek.** Fordon tar olika mycket plats och jag räknar i enheter (3 per plats) för att slippa decimaltal. Är det en bra lösning, eller hade ni gjort på ett annat sätt?
- **Felhantering.** Jag använder knappt exceptions. Felaktig inmatning hanteras med int.TryParse och upprepade frågor, och ett fullt garage eller ett redan parkerat fordon ger ett ParkResult i stället för ett undantag. Är det ett bra sätt, och finns det ställen där programmet fortfarande kan krascha?
- **Att läsa koden.** Vad stannar ni upp över? Namn, kommentarer, sådant som är svårt att följa.
- **Att använda programmet.** Hur känns menyerna, texterna och felmeddelandena när ni provkör? Är något krångligt eller oklart?