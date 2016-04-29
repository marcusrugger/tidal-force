
BIN_DIR			= bin
APP_WINFORMS	= tides-winforms.exe
APP_GTK			= tides-gtk.exe


all:	$(BIN_DIR)
	$(MAKE) -C src
	$(MAKE) -C winforms
	$(MAKE) -C gtk


clean:
	$(MAKE) -C src clean
	$(MAKE) -C winforms clean
	$(MAKE) -C gtk clean


makewinforms:
	$(MAKE) -C src
	$(MAKE) -C winforms


runwinforms:
	mono $(BIN_DIR)/$(APP_WINFORMS)


makegtk:
	$(MAKE) -C src
	$(MAKE) -C gtk


rungtk:
	mono $(BIN_DIR)/$(APP_GTK)


$(BIN_DIR):
	mkdir $(BIN_DIR)
