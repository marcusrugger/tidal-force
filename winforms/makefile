
SOURCE = 	tides.cs				\
			tides.window.cs			\
			tides.toolbar.cs		\
			tides.presentation.cs


ROOT_DIR	= ..
BIN_DIR		= $(ROOT_DIR)/bin
APP_NAME	= tides-winforms.exe
DLL_NAME	= $(BIN_DIR)/tides.dll
TARGET		= $(BIN_DIR)/$(APP_NAME)

PACKAGES	= -pkg:dotnet


all:		$(TARGET)


clean:
	-rm $(TARGET)


$(TARGET):	$(SOURCE)
	mcs -out:$@ -target:exe $(PACKAGES) -r:$(DLL_NAME) $(SOURCE)
