@echo ****HELPER
nugetpublish -bklocal \\eeza.csic.es\nugeteeza$
@echo ****BASIC
cd ..\Rop.Winforms9.Basic
call .\publish.cmd
@echo ****CONTROLS
cd ..\Rop.Winforms9.Controls
call .\publish.cmd
@echo ****KEYVALUELISTCOMBOBOX
cd ..\Rop.Winforms9.KeyValueListComboBox
call .\publish.cmd
@echo ****DECORATORS
cd ..\Rop.Winforms9.Decorators
call .\publish.cmd
cd ..\Rop.Winforms9.Helper




