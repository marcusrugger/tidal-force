
BIN_DIR		= bin
APP_NAME	= tides-winforms.exe


all:	$(BIN_DIR)
	$(MAKE) -C src
	$(MAKE) -C winforms


clean:
	$(MAKE) -C src clean
	$(MAKE) -C winforms clean

run:
	mono $(BIN_DIR)/$(APP_NAME)


$(BIN_DIR):
	mkdir $(BIN_DIR)
