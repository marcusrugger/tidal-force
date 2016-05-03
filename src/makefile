
SOURCE = 	tides.controller.cs				\
			tides.presentation.base.cs		\
			tides.presentation.flatland.cs	\
			animator.base.cs				\
			animator.moon.cs				\
			animator.sun.cs					\
			animator.sunmoon.cs				\
			tidalvectors.cs					\
			forcepoints.cs					\
			forcevectors.cs					\
			constants.cs					\
			gravitationalforce.cs


NUNIT_VERSION		=	nunit-3.2.1
NUNIT_PATH			=	/mnt/data/bin/$(NUNIT_VERSION)
NUNIT_FRAMEWORK		=	$(NUNIT_PATH)/bin/net-4.0/nunit.framework.dll
NUNIT_CONSOLE		=	$(NUNIT_PATH)/bin/nunit3-console.exe

# PACKAGES	= -pkg:dotnet			\
# 		      -pkg:gtk-sharp-2.0

ROOT_DIR	= ..
BIN_DIR		= $(ROOT_DIR)/bin
DLL_NAME	= tides.dll
TARGET		= $(BIN_DIR)/$(DLL_NAME)

PACKAGES	= -pkg:dotnet

FLATLAND_BIN		= ../lib/flatland/bin
FLATLAND_INTERFACES	= flatland.interfaces.dll
FLATLAND_COMMON		= flatland.common.dll
FLATLAND_LIBRARIES	= -r:$(FLATLAND_BIN)/$(FLATLAND_INTERFACES) -r:$(FLATLAND_BIN)/$(FLATLAND_COMMON)


all:		$(TARGET)	\
			runtests


clean:
	-rm $(TARGET)
	-rm tests.dll
	-rm tests.mdb


runtests:	tests.dll
	mono $(NUNIT_CONSOLE) tests.dll -noresult


$(TARGET):	$(SOURCE)
	mcs -out:$@ -target:library $(PACKAGES) $(FLATLAND_LIBRARIES) $^


tests.dll:	test.gravitationalforce.cs	\
			test.forcepoints.cs			\
			test.forcevectors.cs		\
			$(SOURCE)
	mcs -out:$@ -target:library $(PACKAGES) $(FLATLAND_LIBRARIES) -r:$(NUNIT_FRAMEWORK) $^
