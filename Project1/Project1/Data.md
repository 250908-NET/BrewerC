
# Objects:

## Player Record
| Name | Type | Additional Info |
|*|*|*|
| bi_identity | string(uuid) | Not Null, uuid from BI for a player. PRIMARY IDENTIFIER for a player account. Check for 4 at the 13 character. Convert from dashes to no dashes |
ID: 5e740db6-1c81-41ca-89d5-25d13d92fe1c
_4_1ca | The 4 is always in this spot
Remove the dashes before entering into DB


## Player Name
| Name | Type | Additional Info |
|*|*|*|
| bi_identity | string(uuid) |  |
| name | string | 3-32 characters. Check for validity. If longer or shorter, the name is hacked. |

## Server Definition
| Name | Type | Additional Info |
|*|*|*|
| IP Address | string |  |
| Port | int | 0 - 65383 |
| name | string | Server name |


