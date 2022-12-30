.PHONY: main, clear

main:
	dotnet build
clear:
	rm -r AdaCredit/obj
	rm -r AdaCredit/bin
	rm Cities/cities.csproj
