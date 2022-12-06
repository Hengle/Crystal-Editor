# Crystal Editor

Made in C#, Targeting .net 7 Framework.

Crystal Editor is (trying to be) a universal RPG / JRPG editor, modernizing the process of creating romhacks or major mods so simple a child can do it. Users can edit simple values in any game. (This is 99% of most modding projects). Things like Enemy stats, items, abilities, skills, equipment, enemy locations, evolutions, level up learnset, and so on, for *any game*.

As a 'simple' user of Crystal Editor, you can simply select a supported game, Target it on your computer, go to the editor you want, change values, save, and launch the game. It's *that* easy.

As a 'Creator' type user, you can add new games to the supported games list in just 1 click. Then, select a file you want a new editor to be made for. Tell Crystal Editor the file width, and give the editor an appropriate name, and Crystal Editor will *Automatically* create an editor for that file / game. Next, a user can rename anything to more appropriate names, or put things into catagories. For example, one can select all of an enemys elemental resistances and put them into a catagory named "Resistances", so everything is in one location. You can even move the order of things around, like if an enemys stats are in order of Str, Mag, Def, Res, you can reorder it as Str, Def, Mag, Res. The program will still save the data back to the file in the correct order! 

On the inner technical side, Crystal Editor works by the way data is near-universall stored in video games. Regardless of what coding language was used to make a game, or what was done do it, almost every game converts all information to a file format called HEX. a Hex file conveniently, is not only easily readable, but is almost always a table. It's not just "Like" a google spreadsheet table, it is *literally* a table, and you can actually copy them to google sheets. Yes, modding games is really as simple as editing as basic table of information like a google sheet.

Modernly, modders work by copying this info to google sheets, doing everything the hard way, then copying it back. Alternatively, they spend a few years learning a coding language, and making an editor for 1 specific game. The worst part is the documentation teaching people how to do things on their own is awful, and you will almost never find anyone to help you and teach you what to do personally. It being easy doesn't matter, infact it makes it worse. People assume over and over you know more then you do, BECAUSE it's so easy, that they won't even "waste time" teaching these basics. Because of this, the modding scene fucking sucks, it's hard to get into, and the tools are intimidating, and you waste days googleing information just for your first project before giving up because google makes it look hard when it's not. Crystal Editor will be a program that can target *any* table, in *any* game, removing all this hard work, and include a full blown short but effective(short because theres not that much to learn), educational course on how to learn to do it yourself.

Modding is easy, now lets make it accessable to anyone. Together. Help me! :)


PS: This project is not open source. There is because i do not want it to have an open source lisence. This is because I suspect once i get a stable beta off the group, i may want to try selling it (for like 2$ or something) to afford hiring an assistant to make it even greater. If that bothers whoever wants to help me, we can talk about it, or maybe i'll send the money to you instead for helping me. :)
