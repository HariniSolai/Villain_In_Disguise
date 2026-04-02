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
* Bonfires provide localized lighting that enhances atmosphere in the village

**Textures:**

* An old paper texture is used for directional billboards along forest paths
* A skybox texture with clouds provides environmental depth and realism
* Forest Terrain textures include variations such as grass, flowers, dirt, and stone paths to differentiate regions
* Village structures use distinct textures to separate them visually from the natural environment

Lighting and textures are used together to guide the player visually while also establishing the fantasy feel and mood of each area.

---

### AI Systems

The game integrates several AI techniques to support gameplay mechanics and player interaction:

* A Finite State Machine (FSM) controls NPC behavior, including idle states, interactions, and responses to player actions
* Navigation Mesh and pathfinding systems allow NPCs and enemies to move intelligently throughout the environment
* A Bayesian Network is used to model and update the trust system based on player decisions

Additional AI-driven features include:

* Enemy spawning under certain gameplay conditions
* A dynamic trust system that increases or decreases depending on player actions and decision timing

These systems ensure that the game world reacts meaningfully to the player, making decisions impactful.

---

### Animation (Mecanim)

Unity’s Mecanim system is used to animate both characters and environmental elements:

* NPC animations include idle and interaction (talking) states
* Enemy spawn and fight animations when player decides to fight in Q2
* Environmental animations, such as animals(COW) or fire, add ambient motion to the world

These animations improve realism and make interactions feel more natural and engaging.

