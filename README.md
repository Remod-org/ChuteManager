# ChuteManager
Control parachute speed and pickup

Permissions can be set but are optional.

When flying, player can hold the SPRINT key when moving forward or backward to speed things up a bit.

When picking up a chute, the player will not have to wait for the progress bar.

## Configuration
```json
{
  "Settings": {
    "fwdMultiplier": 40.0,
    "revMultiplier": 2.0,
    "AllowFastPickup": true,
    "RequirePermissionForFastPickup": false,
    "RequirePermissionForFastFlight": false,
    "RequirePermissionForCondition": false,
    "PlaySoundOnPickup": true,
    "ExcludeParachuteFromClothingAmount": true,
    "RestoreConditionOnPickup": false
  },
  "Version": {
    "Major": 1,
    "Minor": 0,
    "Patch": 5
  }
}
```

 - fwdMultiplier -- How fast should the deployed chute move FORWARD when pressing SPRINT
 - revMultiplier -- How fast should the deployed chute move BACKWARD when pressing SPRINT
 - AllowFastPickup -- No need to wait when picking up a chute
 - RequirePermissionForFastPickup -- Require chutemanager.pickup permission to use fast pickup
 - RequirePermissionForFastFlight -- Require chutemanager.fast permission to allow fast flight (SPRINT)
 - RequirePermissionForCondition -- Require chutemanager.condition permission to repair the chute on pickup
 - PlaySoundOnPickup -- if true, play the zipper sound, otherwise pickup will be silent
 - ExcludeParachuteFromClothingAmount -- if true, the parachute should not count as a clothing item for the Patrol Helicopter
 - RestoreConditionOnPickup -- if true, the parachute will be fully repaired on pickup

## Permissions
 - chutemanager.fast -- Allows player to use the SPRINT key to speed up forward and backward motion when flying (if RequirePermissionForFastFlight == true)
 - chutemanager.pickup -- Allows player to pickup a chute immediately (if RequirePermissionForFastPickup == true)
 - chutemanager.condition -- Allows player to repair their chute on pickup (if RestoreConditionOnPickup == true)

