# ============================================================================
#  Name	 : build_help.mk
#  Part of  : DateDiff
# ============================================================================
#  Name	 : build_help.mk
#  Part of  : DateDiff
#
#  Description: This make file will build the application help file (.hlp)
# 
# ============================================================================

do_nothing :
	@rem do_nothing

# build the help from the MAKMAKE step so the header file generated
# will be found by cpp.exe when calculating the dependency information
# in the mmp makefiles.

MAKMAKE : DateDiff_0xE8735039.hlp
DateDiff_0xE8735039.hlp : DateDiff.xml DateDiff.cshlp Custom.xml
	cshlpcmp DateDiff.cshlp
ifeq (WINS,$(findstring WINS, $(PLATFORM)))
	copy DateDiff_0xE8735039.hlp $(EPOCROOT)epoc32\$(PLATFORM)\c\resource\help
endif

BLD : do_nothing

CLEAN :
	del DateDiff_0xE8735039.hlp
	del DateDiff_0xE8735039.hlp.hrh

LIB : do_nothing

CLEANLIB : do_nothing

RESOURCE : do_nothing
		
FREEZE : do_nothing

SAVESPACE : do_nothing

RELEASABLES :
	@echo DateDiff_0xE8735039.hlp

FINAL : do_nothing
