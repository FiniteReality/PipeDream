#define THIS_CAN_JUST_BE_A_NAME
#define THIS_MAY_ALSO_HAVE_A "value"
#define SOME(HAVE, PARAMETERS) "but need a value!"
#define PARAMETERS(CAN) CAN ## _concatenated = "Or stringified: " #CAN
#define SOME_DEFINITIONS(_, IGNORE) "parameters"
#define OTHER_DEFINITIONS(...) "accept multiple parameters"
#define DEFINITIONS \
    "can span multiple lines" \
    "like this"
#define THEY_CAN_ALSO #include
#define YES_YOU_HEARD_THAT_RIGHT "they can expand to *other* preprocessor directives."
#undef YOU_CAN_ALSO_REMOVE_DEFINITIONS
