## Auto-Targeting System

## How to Test
* Requires Unity 6.0 (6000.62.f1) or newer
* Open TestScene (Assets/Scnes/TestScene.unity
* Configure Player Auto-Target System Script
* In the Project right click Create->Data to create new weapons , Projectile and Criteria rule (Distance and Low Health at the moment)
* Press Play

## Overview

The Auto-Targeting System automatically identifies valid targets and fires projectiles based on configurable criteria. It supports multiple weapon types and is designed to be fully extensible, allowing new weapons or targeting rules to be added with minimal effort.

- 2D                     // All 2D assets (sprites, textures)
- Materials
- Prefabs
- Scenes
- ScriptableObjects
- Projectiles        // Projectile configuration data
- PriorityCriteria   // Target-selection rules
- Weapons            // Weapon configuration data
- Scripts
- Editor             // Custom editor tools
- Runtime            // Core gameplay codebase
- Settings               // Global game settings and configurations

## Architecture

## Auto Target System

The Player GameObject contains the AutoTargetingSystem script, which is responsible for:

* Scanning for nearby enemies using a configurable search radius.
* Filtering and sorting these targets based on a list of assigned criteria.
* Selecting the highest-priority target.
* Notifying relevant systems (e.g., weapons, visual indicators) when the target changes.

Target scanning is performed using Physics.OverlapSphere combined with a configurable LayerMask to ensure only valid targets are considered.

## Targeting Criteria

Target-selection rules are implemented as ScriptableObjects derived from an abstract class called AttackPriorityRule.
Each rule implements an Apply() method that takes the list of available targets and returns a filtered or sorted list.
This approach allows designers to add, modify, or reorder targeting rules directly in the Inspector without modifying gameplay code, making the system highly modular and scalable.
Example rules in the test project include:
* DistanceRule - Prioritizes targets based on distance.
* LowHealthRule - Prioritizes targets with low remaining health.
Rules support both ascending and descending ordering, depending on designer needs.

## Line-of-Sight Validation
Line-of-sight is handled using a Raycast from the player to the target.
If the Raycast does not hit a collider implementing the IDamageable interface, the system assumes the target is obstructed (e.g., behind a wall) and excludes it from the valid target list.

## Visual Target Indicator (LaserVisual)
The LaserVisual component provides a simple visual indicator by rendering a line from the player to the currently selected target. This serves purely as a debugging or visualization aid.

## Weapons System
Each attack type is represented as a Weapon.
A Weapon consists of:
* WeaponData (ScriptableObject) defining:
* Angle Threshold to attack only when player is looking directly to the enemy.
* Attack Point
The Weapon class handles all firing logic based on the currently acquired target.

# WeaponData (ScriptableObject) defines:
* Weapon type
* Damage
* Range
* Fire rate

Different Weapon types can be created easily, maintaining full modularity.

## Weapon Type Enum
A WeaponType enum is used to define all supported weapon categories.
Using an enum allows:
* Easy selection from an Inspector dropdown.
* Consistent indexing for referencing weapon types.

## Constants
A dedicated Constants script stores all global constant values (strings, numbers, tags, etc.).
Centralizing constants prevents renaming issues and ensures consistency across the project.

## Projectiles
Each weapon uses a configurable projectile, defined via ProjectileData. 

# ProjectileData (ScriptableObject) defines:
* Speed
* Explosion effects
* Impact behavior
* Whether it deals AOE damage
* AOE Radius

Different projectile types (rockets, bombs, bullets, etc.) can be created easily, maintaining full modularity. Projectiles are automatically destroyed when they exit the camera bounds to keep the hierarchy clean.

## AutoDestroy
The AutoDestroy script allows temporary objects (e.g., explosion effects) to be destroyed after a specified duration, ensuring good scene hygiene and preventing clutter.
