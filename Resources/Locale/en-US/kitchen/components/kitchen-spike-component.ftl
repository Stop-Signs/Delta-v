comp-kitchen-spike-begin-hook-self = You begin dragging yourself onto { THE($hook) }!
comp-kitchen-spike-begin-hook-self-other = { CAPITALIZE(THE($victim)) } begins dragging { REFLEXIVE($victim) } onto { THE($hook) }!

comp-kitchen-spike-begin-hook-other-self = You begin dragging { CAPITALIZE(THE($victim)) } onto { THE($hook) }!
comp-kitchen-spike-begin-hook-other = { CAPITALIZE(THE($user)) } begins dragging { CAPITALIZE(THE($victim)) } onto { THE($hook) }!a

<<<<<<< HEAD
comp-kitchen-spike-kill = { CAPITALIZE(THE($user)) } has forced { THE($victim) } onto the spike, killing them instantly!

comp-kitchen-spike-suicide-other = { CAPITALIZE(THE($victim)) } has thrown themselves on a meat spike!
comp-kitchen-spike-suicide-self = You throw yourself on a meat spike!
=======
comp-kitchen-spike-hook-self = You threw yourself on { THE($hook) }!
comp-kitchen-spike-hook-self-other = { CAPITALIZE(THE($victim)) } threw { REFLEXIVE($victim) } on { THE($hook) }!

comp-kitchen-spike-hook-other-self = You threw { CAPITALIZE(THE($victim)) } on { THE($hook) }!
comp-kitchen-spike-hook-other = { CAPITALIZE(THE($user)) } threw { CAPITALIZE(THE($victim)) } on { THE($hook) }!
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

comp-kitchen-spike-begin-unhook-self = You begin dragging yourself off { THE($hook) }!
comp-kitchen-spike-begin-unhook-self-other = { CAPITALIZE(THE($victim)) } begins dragging { REFLEXIVE($victim) } off { THE($hook) }!

comp-kitchen-spike-begin-unhook-other-self = You begin dragging { CAPITALIZE(THE($victim)) } off { THE($hook) }!
comp-kitchen-spike-begin-unhook-other = { CAPITALIZE(THE($user)) } begins dragging { CAPITALIZE(THE($victim)) } off { THE($hook) }!

comp-kitchen-spike-unhook-self = You got yourself off { THE($hook) }!
comp-kitchen-spike-unhook-self-other = { CAPITALIZE(THE($victim)) } got { REFLEXIVE($victim) } off { THE($hook) }!

comp-kitchen-spike-unhook-other-self = You got { CAPITALIZE(THE($victim)) } off { THE($hook) }!
comp-kitchen-spike-unhook-other = { CAPITALIZE(THE($user)) } got { CAPITALIZE(THE($victim)) } off { THE($hook) }!

comp-kitchen-spike-begin-butcher-self = You begin butchering { THE($victim) }!
comp-kitchen-spike-begin-butcher = { CAPITALIZE(THE($user)) } begins to butcher { THE($victim) }!

comp-kitchen-spike-butcher-self = You butchered { THE($victim) }!
comp-kitchen-spike-butcher = { CAPITALIZE(THE($user)) } butchered { THE($victim) }!

comp-kitchen-spike-unhook-verb = Unhook

comp-kitchen-spike-hooked = [color=red]{ CAPITALIZE(THE($victim)) } is on this spike![/color]

comp-kitchen-spike-meat-name = { $name } ({ $victim })

comp-kitchen-spike-victim-examine = [color=orange]{ CAPITALIZE(SUBJECT($target)) } looks quite lean.[/color]
