# WinForms_VuforiaTargetAutoAdder
Windows Forms tool for automatically adding targets to Vuforia's cloud uploader.

> **Note:**  \
> This tool probably doesn't work anymore. I made it in late 2017 to interact with the Vuforia developer website. While I haven't visited the site in a while, I have to assume it has changed in ways break the tool. If you find otherwise, feel free to send me a message.

I made this tool for an AR project in which I needed to upload hundreds of images to Vuforia. At the time, however, the Vuforia site only allowed uploading one image at a time. Faced with such a chore, I thought about alternatives and realized if a human can do something on a computer&mdash;in this case, interacting with a webpage&mdash;so can the computer.

I used Windows Forms to make a web browser with some additional utilities to enable automating Vuforia image uploads. As I recall, it took a while to get it right. Mostly the timing of the interactions, I think. (It's been nearly seven years, so I'm a bit fuzzy.)

There are also two versions of a browser script in the `js` folder: `greasemonkey-script.js` for Firefox's Greasemonkey extension and `tampermonkey-script.js` for Chrome's Tampermonkey extension. The script creates a couple of extra input fields on the Vuforia image-target upload page. The resulting functional page source is in `functional.html`.

Aside from helping me populate my image-target library, this tool let me know how good my images were since you had to upload them to get their ratings. Ultimately, I couldn't get images of high enough quality to be useful for my purposes, and this tool allowed me to figure that out. I wish I had taken a video, but my computer at the time didn't have any video-capturing software.

Perhaps I'll revisit that old AR project one day with a better camera. If so, of course, I'll need to revisit this one, which I think I'd like to do at some point, anyway.
