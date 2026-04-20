# Villain_In_Disguise

Game Idea: Villain in Disguise is a single-player 3D action-adventure game where the player takes on the role of an exiled villain searching for a rare magical resource hidden in a dragon’s lair. To achieve this, the player must secretly protect a town and gain the trust of its NPCs while hiding their true identity. The game focuses on exploration, decision making, and a unique trust system where NPCs judge the player based on actions rather than intentions. Players must balance helping others with avoiding forbidden actions like using dark magic in public, as these choices directly impact progression and access to the final objective.

Key Features of this game include:
- Exploration of forest, town, and wilderness environments
- Trust system that controls NPC behavior and game progression
- “Forbidden Act” mechanic (using dark magic or selfish actions lowers trust)
- Resource collection, crafting (potions/spells), and combat (magic & weapons)
- Multiple outcomes based on player decisions and trust level

# Team 
- Harini Solaidurairaj
- Kahnishga Solaidurairaj
- Omar Khan

## Technical Implementation

### 3D Physics

The project incorporates multiple forms of Unity’s 3D physics to enhance interactivity and immersion within the game world:

* Particle systems are used around magical gems to emphasize their importance and create a visual effect
* Environmental particle systems simulate smoke from chimneys and fire from bonfires
* A force field surrounds the dragon’s lair, acting as a physical barrier tied to game progression
* Player movement includes gravity-based mechanics and controlled flying, allowing for more dynamic exploration

These physics elements help create a more immersive environment and reinforce the game’s magical and interactive aspects.

---

### Lighting and Textures

**Lighting:**

* Glowing Mushrooms act as dynamic light sources within the forest environment
* Spotlight lighting on the Billboards to guide player navigation in the forest
* Bonfire lights provide localized lighting that enhances atmosphere in the village

**Textures:**

* An old paper texture is used for directional billboards along forest paths
* A skybox texture with clouds provides environmental depth and realism
* Forest Terrain textures include variations such as grass, flowers, dirt, and stone paths to differentiate regions
* Bridge and Fence have wood grain texture 

Lighting and textures are used together to guide the player visually while also establishing the fantasy feel and mood of each area.

---

### AI Systems

The game integrates several AI techniques to support gameplay mechanics and player interaction:

* A Finite State Machine (FSM) controls NPC behavior, including idle states, interactions, and responses to player actions
* Navigation Mesh and pathfinding systems allow NPCs to move naturally throughout the village environment.
* A Bayesian Network is used to model and update the trust system based on player decisions

Additional AI-driven features include:

* Enemy spawning under certain gameplay conditions (FSM - question responses)
* A dynamic trust system that increases or decreases depending on player actions and decision timing (Bayesian Network) 

These systems ensure that the game world reacts meaningfully to the player, making decisions impactful with different outcomes/experiances.

---

### Animation (Mecanim)

Unity’s Mecanim system is used to animate both characters and environmental elements:

* NPC animations include idle and interaction (walking) states
* Enemy spawn and fight animations when player decides to fight in Q2
* Environmental animations, such as animals (Cow) or fire flickering, add ambient motion to the world

These animations improve realism and make interactions feel more natural and engaging.

---

### Sounds 
* When the player collects gems I have a magical collection sound which is Leohpaz’s 10_UI_Menu_SFX/079_Buy_sell_01.wav sound from the asset RPG Essentials Sound Effects - FREE! I had chosen this sound as the gem pick up sound effect since there was a noise dispersion like coins falling and that helped paint the picture as in the game when the gems are collected they disappear and with the sound effect it is like the gems get crumbled when collected. Some sound effects I rejected were a glowing orb sound cause it didn't paint the picture that the gems were being collected and were subtle.
Link: https://assetstore.unity.com/packages/audio/sound-fx/rpg-essentials-sound-effects-free-227708

* For all the buttons on the canvas that the player will click we added a Clicking sound. This sound is from SwishSwoosh’s Free UI Click Sound Effects Pack and I decided to specifically use the AUDIO/Button/SFX_UI_Button_Mouse_Thick_Generic_1.wav This sound was chosen as it was more of a mouse click sound that was clear rather than the other keyboard clicking sounds as they weren't as crisp and distinct as a Mouse clicking sound. 
Link: https://assetstore.unity.com/packages/audio/sound-fx/free-ui-click-sound-pack-244644

* Forest General Environment sounds from Nox_Sound’s Asset Nature - Essentials. I specifically selected the Ambiance_Forest_Birds_Loop_Stereo.wav since in forests it's common to be surrounded by critter noises and the birds which felt more natural that the other more popular water ambient forest sounds that were available which I had taken a listen to. Some specific ones I rejected were forest sounds with a lot of water noises like drips since it didn’t work with our forest set up which has more trees and wood. 
Link: https://assetstore.unity.com/packages/audio/ambient/nature/nature-essentials-208227

* Forest General Environment sounds from Nox_Sound’s Asset Nature - Essentials. Part of the game is in a cave location and for our game we wanted a very specific, almost unnerving cave noise. This is because the final boss of the game, a dragon, is residing in the cave. I choose the Ambiance_Cave_Dark_Loop_Stereo.wav to give the cave and area surrounding the cave a spooky feeling. Some sounds I rejected were a cave sound with dripping water, that sound would make sense for an underwater cave or ambient cave noise, but we wanted a cave noise that made the player feel more alert than relaxed. 
Link: https://assetstore.unity.com/packages/audio/ambient/nature/nature-essentials-208227

* The Alerted NPC (Male) - Voice Pack by Voice Bosch. Since an important part of the game is interaction with NPCs, I added NPC voices when the player does certain actions so it feels like they are really interacting with an NPC. (Audio path: Assets/DownloadedAssets/SoundsDownloaded/TheAlertedNPC/24.HeyWhatAreYouDoing). 
Link:  https://assetstore.unity.com/packages/audio/sound-fx/voices/the-alerted-npc-male-voice-pack-301220 

* The Alerted NPC (Male) - Voice Pack by Voice Bosch. Continuing the interaction with NPCs is important. If the player is cordial and polite with the NPC, the NPC should continue being nice and interacting with the player. (Audio path: Assets/DownloadedAssets/SoundsDownloaded/TheAlertedNPC/21.HeyWhatWasThat). 
Link:  https://assetstore.unity.com/packages/audio/sound-fx/voices/the-alerted-npc-male-voice-pack-301220 

* The Alerted NPC (Male) - Voice Pack by Voice Bosch. When the enemy is spawned in, there needs to be something to alert the player of the sudden danger. When looking through the different audios there were very loud or much scarier roars, but I chose the Warning Growl because this enemy is a bit smaller and I wanted to convey danger to the player, but not terrify them. (Audio path: Assets/DownloadedAssets/SoundsDownloaded/MonsterRoars/25.WarningGrowl)
Link: https://assetstore.unity.com/packages/audio/sound-fx/creatures/monster-roars-audio-pack-301118 

* Deadly_Creatures_Pack1_v1 by ShashiRaj Productions. When the player faces off with the dragon we are reaching the climax and end of the game. At this moment, the player is going to fight the dragon. The dragon animations are set, but to give the player the extra layer of realism there needed to be a dragon sound/roar to make the player more alert since they are facing off against a dragon.(Audio path: Assets/DownloadedAssets/SoundsDownloaded/ ShashiRajProductions/Deadly_Creatures_Breathy_Airy_v1_(2))
Link: https://assetstore.unity.com/packages/audio/sound-fx/creatures/deadly-creatures-pack1-v1-280813 

* For the jumping sound, I used a cartoon-style jump sound from Pixabay. I chose this sound because it clearly matches the player’s jumping action and makes movement feel more responsive and noticeable. It adds a light and playful feel to the game. 
Link: https://pixabay.com/sound-effects/film-special-effects-cartoon-jump-6462/ 

* For the church bell sound, I used a bell sound from Pixabay. This sound plays near the church area and helps the player understand their location in the village. 
Link: https://pixabay.com/sound-effects/city-church-bells-194653/

* For the walking/ running sound, I used a looping running sound from Pixabay. This sound plays when the player is moving and helps make the movement feel more realistic. 
Link: https://pixabay.com/sound-effects/film-special-effects-person-running-loop-245173/

* For the cow sound, I used a cow ambient sound from Pixabay. This sound plays when the player is near the cow area and helps make that part of the village feel more alive. 
Link: https://pixabay.com/sound-effects/nature-cows-56001/

* For the background village chatter sound, I used a background chatter sound from Pixabay. This plays in the village to simulate people talking and make the area feel populated. 
https://pixabay.com/sound-effects/people-chattertrainride-17702/

### Additional Gameplay Features (HW7 Updates)

The following improvements were added to enhance gameplay, immersion, and user experience:

* **NPC Wandering System:** NPC characters now actively move around the village using Unity NavMesh. They walk for longer distances and switch between walking and idle states, making the environment feel more alive and dynamic instead of static.

* **Improved Village Interactivity:** The village environment has been expanded with additional houses, shops, and layout improvements to eliminate empty space and create a more realistic and engaging world for the player to explore. Added NPC's walking around the village as mentioned above. Additionally with our interactable NPC we have added interaction questions with buttons for the player to choose a response. 

* **Dragon + Cave:** We created a path from the forest with directional billboards that guide the player into the cave where the dragon is. When the player gains enough trust from the villagers the force field infront of the cave goes away. Which allows the player to enter the cave and fight the dragon with the F key. When they win the screen changes to our win screen. 

* **Mecanim:** Main player animations fighting when "F" button is pressed. Idle annimation when not moving or walking slowly, this animation transitions to running animation when players speed picks up. 
  
* **Teleportation System:** Players can instantly move between major locations using keyboard shortcuts:
  - **B** → Forest  
  - **V** → Village  
  - **C** → Cave  
  This allows faster navigation and smoother gameplay flow.

* **Lose Condition System:** A lose state has been introduced where if the player’s trust level drops below a certain threshold, the game transitions to a lose screen, encouraging strategic decision-making.

* **UI Improvements:** 
  - Added a start screen, instruction screen, and restart/lose screen system for better player feedback  
  - Improved button visibility and interaction clarity (alongside audio clicking feedback)  
  - Enhanced readability of UI elements over the game background
  - Utilized glowing orbs for the gems in the forest so they are more visible to the player along the path  

* **Area-Based Sound System:** Specific sounds now trigger based on player location (e.g., cows near farms, bells near church), making the world feel more responsive and immersive.

* **Environmental Effects Enhancements:**
  - Fire and smoke particle systems improved for realism  
  - Lighting adjustments to better highlight important areas  
  - Added visual effects to key interactive objects  

---

### Assignment 8 Updates

* **Shaders**
  - Surface Shader on gems in the forest 
  - Reflective light Shader on bonfires infront of the cave
  - Red highlighting Shader on the interactable NPC in the village

* **UI Modifications**
  - Created a Credit Screen link after win/lose
  - Updated instruction in game
  - Opening scene with premeise 
  - Throught gameplay we included suggestions for the Player to follow

* **Feedback Testers Provided:**
  - Since the Dark spells option just lowered the players trust and had no other use they wondered what was the usage of creating Potions
  - They wondered why they heard the running noise while also flying 
  - The instructions of where they were supposed to be were unclear since we had a big game board to explore (cave, forest, village)
  - When in the village the testers didn't know which NPC’s were interactable and which weren't
  - When they unlocked level 3 they tried to fight the Dragon but noticed the animation played out regardless of the player actions. So the dragon always died as long as the player pressed the F key to fight once. The testers wanted more fighting interaction and suggested a HP bar to show that progression.
  - In the Village the player went through the houses and fell through the plane resulting in them having to use teleportation keys to get back on the plane. 

* **Alpha Release Feedback based additions**
  - The testers were able to walk off the plane and fell off our playing environment. To fix this issue we added a clear forcefield around the edge of our plane which stops the player from leaving the game environment 
  - We changed the value of each gem from 10 to 1 so players had to forage more through the forest and explore the environment
  - We included three Shaders in our game, The first being a Red lighting Shader on the interactable NPC in the village so it stands out more. The second Shader being a surface Shader on the collectible gems in the forest so they are more prominent against the forest assets. The third Shader is a reflective Shader on the fire pits in front of the cave to create more ambiance to the dragon
  - We completely upgraded the Dragon interaction. The player can cause damage by fighting the dragon (with the F button) or using dark spells to cause damage. The dragon also continuously causes damage to the player, so the player has to respond and fight the dragon to win. There are health bars for visuals on the dragon and player’s health, for better player UI. This animated dragon fight allows for more interaction with the player now. This leads to a better game climax and a more fun experience for the player since they interact with the dragon.
  - Dark spells can be used in the dragon fight and you need potions to make dark spells (in preparation for the fight). Potions can be made using gems. Previously there was no clear use for the potions, but now there is a very clear way the player can use the potions in the game. Players can not make dark potions without potions. 
  - The talk to NPC button activates only when the player is close to the NPC so they know which NPC is interactable. This also makes the gameplay more realistic as the player shouldn’t be able to interact with an NPC they are not close to. 
  - In order to make steps more clear for the player, we added instructions which are on the players screen and follow their gameplay prompting them with more guidance as they continue through the game
  - We updated the instruction scene by providing more detailed information on how to use and maneuver our character.
  - We also added a flying limit to the player so it doesn’t feel like they can escape the map. Additionally, we added background sound for flying. Whenever the player jumps more than twice, they enter flying mode, and a flying sound plays, creating a smoother and more enjoyable experience.
  - We also added background sound for sword attacks, so whenever the player presses “F” to attack, a sword slashing sound will play. This makes the game more interactive, fun, and enjoyable.
