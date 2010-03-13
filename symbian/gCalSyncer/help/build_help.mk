# ============================================================================
#  Name	 : build_help.mk
#  Part of  : gCalSyncer
# ============================================================================
#  Name	 : build_help.mk
#  Part of  : gCalSyncer
#
#  Description: This make file will build the application help file (.hlp)
# 
# ============================================================================

do_nothing :
	@rem do_nothing

# build the help from the MAKMAKE step so the header file generated
# will be found by cpp.exe when calculating the dependency information
# in the mmp makefiles.

MAKMAKE : gCalSyncer_0xE9062F6D.hlp
gCalSyncer_0xE9062F6D.hlp : gCalSyncer.xml gCalSyncer.cshlp Custom.xml
	cshlpcmp gCalSyncer.cshlp
ifeq (WINS,$(findstring WINS, $(PLATFORM)))
	copy gCalSyncer_0xE9062F6D.hlp $(EPOCROOT)epoc32\$(PLATFORM)\c\resource\help
endif

BLD : do_nothing

CLEAN :
	del gCalSyncer_0xE9062F6D.hlp
	del gCalSyncer_0xE9062F6D.hlp.hrh

LIB : do_nothing

CLEANLIB : do_nothing

RESOURCE : do_nothing
		
FREEZE : do_nothing

SAVESPACE : do_nothing

RELEASABLES :
	@echo gCalSyncer_0xE9062F6D.hlp

FINAL : do_nothing
