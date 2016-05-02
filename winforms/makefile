
SOURCE = 	tides.cs				\
			tides.window.cs			\
			tides.toolbar.cs


ROOT_DIR	= ..
BIN_DIR		= $(ROOT_DIR)/bin
APP_NAME	= tides-winforms.exe
DLL_NAME	= $(BIN_DIR)/tides.dll
TARGET		= $(BIN_DIR)/$(APP_NAME)

PACKAGES	= -pkg:dotnet

FLATLAND_BIN		= ../lib/flatland/bin
FLATLAND_INTERFACES	= flatland.interfaces.dll
FLATLAND_COMMON		= flatland.common.dll
FLATLAND_CAIRO		= flatland.gdiplus.dll
FLATLAND_LIBRARIES	= -r:$(FLATLAND_BIN)/$(FLATLAND_INTERFACES) -r:$(FLATLAND_BIN)/$(FLATLAND_COMMON) -r:$(FLATLAND_BIN)/$(FLATLAND_CAIRO)


all:		$(TARGET)


clean:
	-rm $(TARGET)


run:		$(TARGET)
	mono $(TARGET)


$(TARGET):	$(SOURCE)
	mcs -out:$@ -target:exe $(PACKAGES) -r:$(DLL_NAME) $(FLATLAND_LIBRARIES) $(SOURCE)
