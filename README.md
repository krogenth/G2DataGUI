## A Graphical version of G2Data

Grandia 2 Data reading/writing
 - Programming by [krogenth](http://steamcommunity.com/profiles/76561198163774445)
 - Thanks to [woog](http://steamcommunity.com/profiles/76561198008094982/) for providing most data entries

## What this does:

This is a C++ GUI program(MSVC 2019) using DX11. Currently supports:

MS_PARAM.BIN

SK_PARAM.BIN

TB_MAGIC.BIN

TB_SKILL.BIN

TB_SPCL.BIN

ITEM.BIN

PC_INIT.BIN

Enemy Data Files(excluding boss move tables)

## How to use:

1. Move the program to the game root directory, right where grandia2.exe is(steamapps/common/Grandia II Anniversary Edition).
2. Back-up the content/data/ in case anything gets messed up
3. Run the program
4. Edit values as desired
5. Save

## Other information:
	
For those seeking information on files not handled yet(particularly \*.pvp, \*.nj, \*.dat, etc.) check out [this Github repo](https://github.com/bogglez/Ninja-Lib/tree/master/documentation).

For \*.nj files(data/afs/map/\*/\*.chr, data/afs/model/\*.dat) check out [this](http://sharnoth.com/psodevwiki/format/nj).

For \*.njm files(data/afs/map/\*/\*.chr, data/afs/map/\*/\*.chr2) check out [this](http://sharnoth.com/psodevwiki/dreamcast/njm).
