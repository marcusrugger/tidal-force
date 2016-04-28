
SOURCE = 	tides.cs						\
			tides.window.winforms.cs		\
			tides.toolbar.winforms.cs		\
			tides.controller.cs				\
			tides.presentation.base.cs		\
			tides.presentation.gdiplus.cs	\
			animator.base.cs				\
			animator.moon.cs				\
			animator.sun.cs					\
			animator.sunmoon.cs				\
			algorithms.cs					\
			tidalvectors.cs					\
			forcepoints.cs					\
			forcevectors.cs					\
			constants.cs					\
			cartesian.cs					\
			polar.cs						\
			gravitationalforce.cs

NUNIT_VERSION		=	nunit-3.2.1
NUNIT_PATH			=	/mnt/data/bin/$(NUNIT_VERSION)
NUNIT_FRAMEWORK		=	$(NUNIT_PATH)/bin/net-4.0/nunit.framework.dll
NUNIT_CONSOLE		=	$(NUNIT_PATH)/bin/nunit3-console.exe

# PACKAGES	= -pkg:dotnet			\
# 		      -pkg:gtk-sharp-2.0

APP_NAME	= tides.exe
PACKAGES	= -pkg:dotnet


all:		tides.exe	\
			tests.dll	\
			runtests


clean:
	-rm tides.exe
	-rm tests.dll
	-rm tests.mdb


runtests:	tests.dll
	mono $(NUNIT_CONSOLE) tests.dll -noresult


run:		$(APP_NAME)
	mono $(APP_NAME)


$(APP_NAME):	$(SOURCE)
	mcs -out:$@ -target:exe $(PACKAGES) $^


tests.dll:	test.polar.cs				\
			test.cartesian.cs			\
			test.gravitationalforce.cs	\
			test.forcepoints.cs			\
			test.forcevectors.cs		\
			test.algorithms.cs			\
			$(SOURCE)
	mcs -out:$@ -target:library $(PACKAGES) -r:$(NUNIT_FRAMEWORK) $^
