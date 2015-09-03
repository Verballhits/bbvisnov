# BB VisNov

BB VisNov is a game engine for visual novel style games.

All story resources are purposely kept out of the code and are not compiled to allow story creators to create stories and add them to the engine without having to compile the engine.

## Stories

Stories are individually contained playable games (stories).  
All stories are contained in the Content/Stories folder. Game resources such as backgrounds, characters, music and sounds required for the story are also contained within the story folder.  

A story consists of a descriptive *.story.xml file and a corresponding subfolder:
```
Stories/<storyname>.story.xml
Stories/<storyname>/
```

A basic story folder structure would be as follows:
```
Stories/<storyname>/Dialogs/
Stories/<storyname>/Graphics/
Stories/<storyname>/Graphics/Backgrounds/
Stories/<storyname>/Graphics/Characters/
Stories/<storyname>/Graphics/Items/
Stories/<storyname>/Items/
Stories/<storyname>/Music/
Stories/<storyname>/Scenes/
Stories/<storyname>/Sounds/
```

Scenes are how you define the main story flow (a bit like pages). A story will typically contain many scenes.  
You transition between scenes mainly through clickable areas in the scenes.  

Scenes also contain characters and dialog trees of conversations between the characters.  
These characters and dialogs can be encountered while transitioning and interacting with the scenes.  

## Story Resource Formats

Scenes and Dialog Trees are *.xml files.  
Background, Character and Item images are *.png or *.jpg files.  
Music and Sounds are *.ogg files.  

For examples of the format in the scenes and dialogs xml files see the sample story provided.  

## Build

To build from source all you need is:
- Visual Studio 2015 (Community Edition)
- MonoGame SDK 3.4
