# KillTrackr
A BepInEx mod for Among Us that displays a kill feed showing who killed who and where.

## Installation
1. Install [r2modman](https://thunderstore.io/package/ebkr/r2modman/)
2. Install BepInEx via r2modman
3. Drop `KillTrackr.dll` into your BepInEx plugins folder

## Building from source
1. Install [.NET 6 SDK](https://dotnet.microsoft.com/download)
2. Clone the repo
3. Run `dotnet build Plugin.csproj`
4. The dll will be copied to your plugins folder automatically (edit the path in Plugin.csproj first)

## Features
- Shows a kill feed in the center of the screen when a kill happens
- Displays killer, victim, and room name
- Entries fade out after 5 seconds

## Notes
- For use in freeplay/private games only
- Your fault if you're banned from public servers for cheating. This is not for public.
