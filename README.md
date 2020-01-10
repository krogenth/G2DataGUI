A Graphical version of G2Data

Grandia 2 Data reading/writing
 - Programming by krogenth: http://steamcommunity.com/profiles/76561198163774445
 - Thanks to woog for providing most data entries: http://steamcommunity.com/profiles/76561198008094982/

What this does:

This is a C++ GUI program(MSVC 2019) using DX11. Currently supports:

MS_PARAM.BIN(moves)

#How to use:

1.) Copy the content/data/ folder to where the program is, or move the program to the game root directory, right where Grandia2.exe is(steamapps/common/Grandia II Anniversary Edition).

2.) Back-up the content/data/ in case anything gets messed up

3.) Run the program

4.) Edit values as desired

5.) Save

#Other information:

I learned a few things messing around with the files:

MS_PARAM.BIN:

The game reads this file literally. It does not care about the ID at the start of the spell/move, it might as well not exist. However, you can safely remove any empty entries at the end of the file(after Taint of WInd), and the game will still work perfectly. If you try to remove the empty entries between spells/moves, then the spells/moves will be off-place(i.e. - if you remove 1 entry before Tenseiken Slash, Ryudo will have a start of Flying Tenseiken, Purple Lightning, and Sky Dragon Slash, with the old Sky Dragon Slash being an empty entry.

SK_PARAM.BIN:

Same thing as MS_PARAM.BIN, it's entries are literal, only trailing empty entries can be removed.

PC_INIT.BIN:

The last 6 entries are just copies of Ryudo, all 6 can safely be removed.

ITEM.BIN:

There are 3 different sections: the initial entry of the item, and 2 different types of "definitions." The definitions are for different types of items, one being for equipment, the other for usable items. Some equipment can have both an equipment definition and item definition.
	
For those seeking information on files not handled yet(particularly *.pvp, *.nj, *.dat, etc.) check out this Github repo:
https://github.com/bogglez/Ninja-Lib/tree/master/documentation

For *.nj files(data/afs/map/*/*.chr, data/afs/model/*.dat) check out:
http://sharnoth.com/psodevwiki/format/nj

For *.njm files(data/afs/map/*/*.chr, data/afs/map/*/*.chr2) check out:
http://sharnoth.com/psodevwiki/dreamcast/njm
