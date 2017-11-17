# Documentation

To start to using this utility, you must first have a directory which contains all your USFM files to check. The program will only run against one file extension so all the USFM files need to have the same extension (e.g., `.SFM` or `.PTX`). Make sure that only those files which contain the books of the bible (Genesis through Revelation) are using that extension.

You will have problems if you have other non-bible files in the directory with the designated extension. Please note, this tool is not really a USFM format checking tool. You may find some of the tools in this utility helpful for finding bogus tags or long words that really are missing a space between them, but this tool really is expecting properly formatted USFM files as input.

### *Selecting the USFM files to Test/Convert/Check*

Start the **GoBibleCreatorUSFMPreprocessor** (**GBC_Tool** from here on out) utility. To get started, select the button in the top left corner with the "..." to open a directory selector tool. Navigate to your directory on your machine that has the USFM files that you want to check.

Use the **USFM file extension**"dropdown to select the correct file extension that corresponds your USFM files.

Once you have done this, you are now ready to use most of the utilites on any of the tabs. However, for things like the **Export to DigiStudy format**, you really do need to have clean, extra tag free files.

### *Converting from one CodePage to UTF-8 Format*

For **GoBibleCreator** to work properly, you must convert your regular ASCII text that uses a codepage to a Unicode compliant text file (UTF-8). If you are already working with UTF-8 formatted files, then skip this step. The following utility should be able to do that without any problems as long as you specify the proper codepage that your files are in. Notice that your original files will not be altered and new files will be created. These files will have a new extension (.utf by default). Press the **Convert Files to UFT-8** button to process the conversion.

### *Grabbing Bible Book Names*

When you are making a collection's file for GoBibleCreator [url:], one of the tasks that you are required to do is create the book name tags in the file. GoBibleCreator is very fussy about this and requires that the book names specified in the collections file match up exactly with the book name specified in the USFM document. If they don't, GoBibleCreator will quit and give you and error. To help in creating that collections file, there is a function which will go into each of your USFM files and extract the book name for you and create the correct format for you to cut-and-paste into your file.

About the only thing you need to specify is the correct tag in your USFM which holds the book name information (the `\h` tag is the typical one). By pressing the **Grab Book Names** button, the program will snag all the book names from the files. You can then use the **Copy to Clipboard** button to copy all the results from the left-hand pane to the clipboard for pasting in your **GoBibleCreator** collections file. There is a toggle button to let you select how many comments you want added to the snippet.

**Note:** you will still need to go into the collections file and order the books in the order you want. Book display order is specified there.

### *Extra Tags Processor*

**GoBibleCreator** only will process the standard tags that have been specified in the USFM reference manual [url:]. However, **ParaTExt** allows the individual user to create their own tags for their specific situation. While this is a good thing, it will cause problems in **GoBibleCreator**, as it doesn't know what to do with these tags. Therefore, this function will let help you preprocess these tags out of your document so that it doesn't pose a problem when you get around to converting.

By pressing the **Scan for Tags that will not be Processed by GBC (UTF-8 Files Only)** button, the program will go through all your USFM files looking for tags that are not standard USFM ones. Once completed, it will dump the results into the grid for you to deal with. The number of occurances for each tag is kept (note that it will count tags that have start and end tags twice) as well as the names of the files where this tag was found (right pane as you move your mouse over a tag in the grid).

At this point, you have five options in how you want to deal with the tag:

1. **IGNORE TAG** - does what it says, lets you ignore the tag.
Note that this tag will show up in your final bible text on GoBible if you select this.
2. **Remove Singular Tag - Remove Tag Only** - This selection is designed to remove tags that only have a start tag and not an end tag. With this command, the program will remove only the tag from your USFM files but leave everything that surrounds the tag.
3. **Remove Singular Tag - Remove Tag and Remainder of line** - This selection is designed to remove tags that only have a start tag and comments that take up the whole line. With this command, the program will remove from your USFM files the tag itself and the rest of the line to the right of it.
4. **Remove Double Marker Tags (Only) - leave text in between** - This selection is designed to remove tags that have both a starting and ending tag (e.g., `\ui ... \ui*` ). All text between the tags is left as valid bible text that you want to show up in GoBible.
5. **Remove Double Marker Tags (Full) - remove text in between** - This selection is designed to remove tags that have both a starting and ending tag (e.g., `\ui ... \ui*` ) and all text between the tags.


In the rightmost cell of each grid, there is a dropdown that lets you control the action of how you want that tag processed.


### *Check For Consistency*

The main feature of this tab will allow you to search for versification issues. You can paste the results from your clipboard in order to view them.

Another feature of this tab is to search for long words.

> *In **GoBible v2.3.4** there was a bug whereby if there was a long word, the application would wrap it and move it to the next line but it would also cut off one letter while wrapping it. Therefore this tab allows you to check for*
> - *long words*
> - *long words with hyphens*
> - *long words with hyphens and attached punctuation*
> 
> *Choose desired number of letters for long words (the default is 15) and choose one of those options. Then click the **Find All Long Words** button. Generated results will be placed in clipboard. You can use Excel™ to paste and view the results.*

### *Export to BQ/DigiStudy Format*

This tab will allow you to export your USFM files into a DigiStudy or BibleQuote format. First make sure you filled out all the fields on this tab.
Fill out Bible Name Full, Bible Name Short, and Copyritgh. Bible Name Full is what will appear in the drop-down list of available bibles of DigiStudy. Then specify whether the language of the bible is Left-to-Right or Righ-to-Left. Then check other Radio-buttons: if it's a Bible, if it has both Old and New Testaments, if it has Apocrypha books, and if it has Greek or not.

On the following line specify the entire alphabet of the language of the Bible. Then select Code Page and Desired Font CharSet from drop-down lists.

On the next line specify HTML tag that will be used as a beginning of the chapter sign and beginning of new verse sign. In the bottom right corner type what tag is used in USFM files to specify Bible book name.

For languages that have specific characters check a Swap Characters for HTML friendly codes check-box in the bottom left corner. Another form will pop up. There you will find instructions for typing in specific characters and corresponding HTML code. You can use an existing character definition code in .xml format by pressing Open Existing Char Definition File button or save the characters and HTML codes that you type in by pressing Save Char Definition File button.

Close this form when your are ready. All character definitions will be saved in memory and used when you start to generate BQ export. To export the Bible press **Generate BQ Export** button. Wait for a couple of minutes for it to finish.

### *Export to ePub Format*

These are the steps to use the Epub Export Feature of USFM Preprocessor:
First, make sure your .sfm files are formatted correctly or in a way that makes them compatible with Preprocessor requirements for .sfm files.

To do that click Open Folder |…| button in the upper left corner and choose a folder containing your .sfm files. Then open Extra Tags Processor tab and click the button **Scan the Tags that will not be processed…**. Make sure that USFM File Extension drop down above has the right extension selected.

In this case scanning showed that there are a lot of closing `\fr*` tags (USFM Preprocessor uses one of the earlier versions of USFM Reference as a standard, which didn’t support closing tags for the `\fr ` and some other tags).

In order for the program to process epub file correctly, you’ll need to get rid of these tags. To do that, choose **Remove Singular Tag** from a drop down menu next to the tag:

Do this for each tag and then click **Process Tags and Create New Files** button in the bottom.As a result you will have a new set of files with .utf extension, if you didn’t change the default.

Next switch to the **Export as ePub** tab and choose utf file extension in the USFM File Extension drop down above. Now you’re ready to import your .utf or .sfm (if you didn’t need to change anything on the previous step) files into epub format.

Now you need to set your books in the right order. To do that, click **Set Book Order** button in the upper right corner.

In the Set Book Order window that will open click Grab Books button. If your book name tag is not `\h`, then change it. As a result the grid will be filled out with your .utf or .sfm files in existing order. You can drag files up and down to put your books in desired order. Or you can click on **Load Existing Book Order** to load the order you saved previously. When you’re done putting your books in order, click **Save Order**, in the bottom of the window.

A message will pop up with the name of your book order text file.

Now click **Load Books from book order file** button on the main screen. As a result the grid below the button will be filled with the names of the files. At this point you can select the files you would like to output in ePub format. You can also reorder the files if you are not satisfied with the order. You can select all the files by clicking on the grid header for the first column. It says **Select All**.

Then fill out all the fields to the right of the grid. Check **Paragraph mode** check box in the bottom if you want the text come in paragraphs. Provide Cover Page Title, Bible Name, Description, ISBN, Publisher info and so on in the **Title Page info** text field.

If your Bible has introduction for each book of the bible, provide the name of this introduction in your language in the **Intro Name** field.

There are two more optional things that you will need for your epub file. They have to do with formatting: cover page image and css file. If you want to include them, download two additional folders in a zip file css and images.zip.

Paste them into the folder with your .utf/.sfm files. Then go inside **/images** folder and replace image with your own. Open common.css file inside **/css** folder and modify it the way you like.

#### Embedding the fonts

If your language doesn't properly display in the Epub reader application, then you have an option to embed the fonts. Embedding the fonts involves: 

- including a 'fonts' folder along with yous css folder, on the same level as the css folder, and

- changing your css file, to reference and apply the font of your choice. You can download a css example with embedded fonts here [fonts_and_css.zip](fonts_and_css.zip) - it's a zip archive that includes both css and fonts folder.

- If you're using embedded fonts unzip this file into your working directory, replacing css folder with this one, if necessary. Then replace the fonts inside the fonts folder with the font files of your choice and edit common.css inside **/css** folder. In case USFM Preprocessor application comes across the **/fonts** folder, it automatically embeds this font into your epub. But you need to make sure your .css file has all the necessary references to it, as in the sample css you've downloaded.

Now you’re ready to process your files. Click on **Generate ePub Export** and wait for the files to process. You will find your .epub files with the name you gave it in the EPUB file name field in the same folder with your original .sfm files.

We recommend using **Calibre** epub reader on a PC and **Moon+** Reader on Android tablets.

To look inside, edit, and repackage your epub file we found it the most helpful to use **Sigil Epub Editor** found [here](http://code.google.com/p/sigil/).

#### Revision history

*Last updated on CodePlex: Nov 30, 2012 at 4:32 AM by anyalee, version 22*

*Text transcribed and formatted for GitHub: 11/17/2017 7:20:50 PM  by David Haslam.*

##### TBD
- Insert images from the original [Documentation](https://gbcpreprocessor.codeplex.com/documentation)
- Fix links where required.