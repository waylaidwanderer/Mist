## Info ##

**BEFORE YOU GET STARTED:** This program requires you use git to download the program.  **Downloading the zip will not work!**  It doesn't work because the program uses git submodules, which are not included with the zip download.

**Mist** is a portable Steam client that allows you to chat with friends and trade TF2 items anywhere. It is based heavily on [SteamBot](https://github.com/Jessecar96/SteamBot) and relies on many of its functions. The program is publicly available under the MIT License.

## Configuration Instructions ##

### Step 0 ###
If you've just recently cloned this repository, there are a few things you need to do in order to build the source code.

1. Run `git submodule init` to initalize the submodule configuration file.
2. Run `git submodule update` to pull the latest version of the submodules that are included (namely, SteamKit2).
 - Since SteamKit2 is licensed under the LGPL, and Mist should be released under the MIT license, SteamKit2's code cannot be included in Mist.  This includes executables.  We'll probably make downloads available on GitHub.
3. Open `Mist.sln` in your C# development environment; either MonoDevelop or Visual Studio should work.
4. Build the program.

Mist is licensed under the MIT License.  Check out LICENSE for more details.

## Wanna Contribute? ##
Please read [CONTRIBUTING.md](https://github.com/waylaidwanderer/Mist/blob/master/CONTRIBUTING.md).
