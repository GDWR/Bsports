{
  inputs.nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";

  outputs = { self, nixpkgs }:
    let
      forAllSystems = function:
        nixpkgs.lib.genAttrs [ "x86_64-linux" "aarch64-linux" ]
        (system: function nixpkgs.legacyPackages.${system});
    in {
      devShells = forAllSystems (pkgs: {
        default = pkgs.mkShell {
          nativeBuildInputs = with pkgs; [
            # aot
            nix-ld
            dotnet-sdk_9
          ];

        NIX_LD_LIBRARY_PATH = with pkgs; lib.makeLibraryPath ([
          # aot
          stdenv.cc.cc
          dotnet-sdk_9
        ]);
        LD_LIBRARY_PATH = with pkgs; lib.makeLibraryPath ([
          # aot
          stdenv.cc.cc
          # libskiasharp
          fontconfig
          xorg.libX11
          xorg.libICE
          xorg.libSM
          dotnet-sdk_9
        ]);
        NIX_LD = "${pkgs.stdenv.cc.libc_bin}/bin/ld.so";
      };
    });
  };
}
