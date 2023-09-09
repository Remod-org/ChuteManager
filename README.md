# ChuteManager
Control parachute speed and pickup

## Configuration
```json
{
  "Settings": {
    "fwdMultiplier": 40.0,
    "revMultiplier": 2.0,
    "AllowFastPickup": true,
    "RequirePermissionForFastPickup": false,
    "RequirePermissionForFastFlight": false,
    "PlaySoundOnPickup": true
  },
  "Version": {
    "Major": 1,
    "Minor": 0,
    "Patch": 2
  }
}
```

 - fwdMultiplier -- How fast should the deployed chute move FORWARD when pressing SPRINT
 - revMultiplier -- How fast should the deployed chute move BACKWARD when pressing SPRINT
 - AllowFastPickup -- No need to wait when picking up a chute
 - RequirePermissionForFastPickup -- Require chutemanager.pickup permission to use fast pickup
 - RequirePermissionForFastFlight -- Require chutemanager.fast permission to allow fast flight (SPRINT)
 - PlaySoundOnPickup -- if true, play the zipper sound, otherwise pickup will be silent

## Permissions
 - chutemanager.fast -- Allows player to use the SPRINT key to speed up forward and backward motion when flying (if RequirePermissionForFastFlight == true)
 - chutemanager.pickup -- Allows player to pickup a chute immediately (if RequirePermissionForFastPickup == true)
