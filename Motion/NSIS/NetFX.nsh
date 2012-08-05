!ifndef NETFX_INCLUDED
!define NETFX_INCLUDED

!include WordFunc.nsh
!insertmacro VersionCompare
!include LogicLib.nsh

Function GetDotNETVersion
  Push $0
  Push $1
 
  System::Call "mscoree::GetCORVersion(w .r0, i ${NSIS_MAX_STRLEN}, *i) i .r1 ?u"
  StrCmp $1 0 +2
    StrCpy $0 "not found"
 
  Pop $1
  Exch $0
FunctionEnd

 ; GetParentDirectory
 ; input, top of stack  (e.g. C:\Program Files\Poop)
 ; output, top of stack (replaces, with e.g. C:\Program Files)
 ; modifies no other variables.
 ;
 ; Usage:
 ;   Push "C:\Program Files\Directory\Whatever"
 ;   Call GetParent
 ;   Pop $R0
 ;   ; at this point $R0 will equal "C:\Program Files\Directory"
Function GetParentDirectory
  Exch $R0
  Push $R1
  Push $R2
  Push $R3
  
  StrCpy $R1 0
  StrLen $R2 $R0
  
  loop:
    IntOp $R1 $R1 + 1
    IntCmp $R1 $R2 get 0 get
    StrCpy $R3 $R0 1 -$R1
    StrCmp $R3 "\" get
  Goto loop
  
  get:
    StrCpy $R0 $R0 -$R1
    
    Pop $R3
    Pop $R2
    Pop $R1
    Exch $R0    
FunctionEnd

!macro CompareDotNETVersion _STRINGVERSION _RESULT
	Call GetDotNETVersion
	Pop $0
 	${If} $0 == "not found"
		StrCpy ${_RESULT} -1
	${Else}
		StrCpy $0 $0 "" 1 # skip "v"
		${VersionCompare} $0 ${_STRINGVERSION} ${_RESULT}
		${If} ${_RESULT} == 2
			StrCpy ${_RESULT} -1
		${Endif}
	${EndIf}
!macroend

!macro InstallDotNETFx _NETFXFILE _RESULT
	IfFileExists "${_NETFXFILE}" exist notexist
	exist:
    MessageBox MB_YESNO ".NET Framework 2.0 or higher version is required. $\n Would you install .NET Framework 2.0 now?" IDYES true IDNO false
    true:
		nsExec::ExecToStack '"${_NETFXFILE}"'
		StrCpy ${_RESULT} 1
		goto done
		false:
		StrCpy ${_RESULT} -1
		goto done
	notexist:
		StrCpy ${_RESULT} -1
	done:
!macroend

!endif
