#if EXPECTS_A_VALUE
var/and = "turns a block on or off"
#endif

#if !NEGATED
var/conditions = "also work"
#endif

#if CAN(ACTUALLY) && BE(QUITE) || NESTED
var/cool = "huh?"
#endif

#if defined(THING)
var/will = "only be enabled if THING is defined"
#endif

#ifdef THING
var/this = "will be seen if THING is defined"
#endif

#ifndef THING
var/this = "will be seen if THING is undefined"
#endif

#if THING
var/this = "will be seen if THING is defined and truthy"
#else
var/this = "will be seen otherwise"
#endif

#if THING
var/this = "will be seen if THING is defined and truthy"
#elif OTHERTHING
var/this = "will be seen if OTHERTHING is defined and truthy"
#endif

#if THING
var/this = "will be seen if THING is defined and truthy"
#elif OTHERTHING
var/this = "will be seen if OTHERTHING is defined and truthy"
#else
var/this = "will be seen if neither of the above cases happened"
#endif
