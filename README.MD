# TikTok Reddit Bot
### This is a bot that can make typical reddit videos from a subreddit to be uploaded to TikTok.

##### The program scraps through a given subreddit and finds the top rated comments for each of them then renders a video using the screenshots (5 comments per video).

## Technologies used:
 - .NET core 3.1 (*C#*)
  - Newtonsoft JSON library for C# (*Newtonsoft.Json*)
  - Microsoft T2S library for C# (*System.Speech*)
  - FFMPEG
 - pyhton3 (*Selenium*, *Pillow*, *BeautifulSoup4*)
  - Chrome Webdriver

## Usage:
 1) Clone the repository.
 2) Download the [FFMPEG library executables](https://ffmpeg.org/download.html "FFMPEG download") and copy the files into the ``ffmpeg`` folder. 
 3) Download a [ChromeDriver](https://chromedriver.chromium.org/downloads "ChromeDriver download") that is comaptible with your Chrome browser and copy the file into the ``resources/py`` folder.
 4) Download ``pyhton3``.
 5) Navigate to the ``resources/py`` folder and run the ``pip install -r requirements.txt`` command.
 6) Set up the [``resources/settings.json``](#settings) and [``resources/cursewords.json``](#cursewords) to satisfy your needs.
 7) Download the [Newtonsoft JSON binary](https://www.nuget.org/packages/Newtonsoft.Json/ "Newtonsoft JSON NuGet Gallery") and copy the .dll file into the root folder.
 8) Download the [Microsoft T2S binary](https://www.nuget.org/packages/System.Speech/ "Microsoft T2S NuGet Gallery") and copy the .dll file into the root folder.
 9) Compile the code and run the compiled executeable.
 10) You will find the videos that are rendered in the ``done`` folder.
 
 ## <a name="settings">Settings:
 You can find three objects in the json file:
 - **backgroundVideoPath**: the relative path to the video that you want the background to be.
 > **Note:** this video have to be somewhat long due to the bot can not loop a short video. The start time is randomly selected.
 - **subredditUrlOrFileName**: The link to the subreddit that you want videos from. Or download the RSS file and copy it to the root folder.
 > **Note:** if you are using an RSS file leave the .rss extension. Also you can use subreddit sorting such as ``/top/``.
 - **numberOfVidsPerPost**: Number of vids per subreddit.
 
 #### Example ``settings.json``:
 ```json
 {
	"backgroundVideoPath": "background/minecraftParkourVideo.mp4",
	"subredditUrlOrFileName": "https://reddit.com/r/askreddit/top/",
	"numberOfVidsPerPost": 3
}
 ```
 
 ## <a name="cursewords">NSFW word censor:
 You can also choose the words that you want censored out. There is a ``cursewords.json`` file in the ``resources`` folder. The name of the json object is the word that you want to replace and the value is the word that you want it to be replaced with.
  #### Example ``cursewords.json``:
 ```json
 {
	"cat": "meow",
	"dog": "bark",
	"food": "drink"
}
 ```
 
 ## ETC:
 - There is a ``clear.bat`` int the ``resources`` folder. Recommended to use after the bot rendered a full subreddit and you want to make videos from the same one but with another sorting becouse the files may interfere with eachother.
 - For compilation I would recommend [Mono](https://www.mono-project.com/ "Mono website"). An open source implementation of Microsoft's .NET Framework.
 - If you downloaded the NuGet packages (.nupkg) for the .dll files you have to decompress them and find the suitable for the .NET 3.1 core. Any other versions may result in unintended behaviour.
 
