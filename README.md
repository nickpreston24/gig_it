# GIT IT

__A TOOL FOR GIG WORKERS TO MAXIMIZE THEIR EARNINGS__

## Checklist

### Authentication

- [ ] Find google auth integration or use firebase, for starters.

### Maps

- [ ] Find google maps cdn or plugins for .net. fallback will be your maps from `kiyapp`
- [ ] Shortest path is fine, but I'd like to have maps find the lowest traffic and least turns (favoring right-hand
  turns). It would be cool if we could rank likely trips by how many turns and how much traffic (by the hour), best
  times to do runs for a particular store, etc.

### My Features Wishlist

1. Heatmaps.
2. Show user what's within 2.5, 5mi and 10mi, etc. of their house.
3. Show user how much $$ it's costing them to move from one power strip to another, how much it costs for a return trip,
   etc, before they make the run. See if you can use DoorDash's drive simulation API to get realistic metrics.
4. ???
5. Profit.

### My Ideal Coding Setup for this project

- [ ] sql file names are callable, using static, named Resources (`Resources.cs`)
- [ ] Maybe make them enumerations, if it makes sense. If not, no worries, they're static anyways.
- [ ] Make calls to `sharpify generate -i ./Models/json/ -o ./Models` on Build Step in your `.csproj`
- [ ] Create a special service that reads all bases in an airtable and creates sql tables from them.

### Fixes

- [ ] Try embeds from scratch again
- [ ] Test embeds on SQL files.