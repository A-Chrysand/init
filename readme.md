# init porgram

### temp
|fileName|function|target|
|:----:|:----:|:----:|
| ce.py  | set JAVAHOME | full remake|
| ide.exe | quick run ide| add config file support|
| ipa.bat | ipconfig /all| full remake|
| vi.ps1| quick nodepad.exe edit file| full remake|
|vmrun.py| start vm in cmd(used in SSH)| full remake|




## description

This is just only some/something quick launch command SET program.  
<em>Actually this program's functions are THE SAME AS the bat/ps1 shell script or edit Windows Register table</em>   
Because ```.bat``` is so awful and ```.ps1``` is not able to run directly in Win+R (you need to
run ```powershell xxx.ps1``` instead of ```xxx.ps1```)    
### file description
Program: ```%dir%/init.exe```, used to execute some commandline command.  
Conifg: ```%dir%/config/init.ini```, used to storage some custom

## usage

1. Set your custom cmd in ```config/init.ini```.  
    In ``init.ini``, every things are like common config file, one section must contain 3 keyname: ```AlterName```,```ExecuteCmd```,and at least 1 custom command.  
    Tips:
    1. ```AlterName``` and ```ExecuteCmd```  themselves are case sensitive. Please pay attention to the case while editing the file.  
    2. Path are supposed to add " " like ```"D:\\software\\test.exe"```(√) instead of ```D:\\software\\test.exe```(×) 
   



```ini
[test]
AlterName = t,test,test0,exampleTest,    #[required] altername is use to execute as parameter
ExecuteCmd = game111,cmdtwo...           #[required] cmd is the all [commandline commands] will be executed and linked to the name you set below
game111 = "G:\\steam\\steam.exe"         #[required] ### You can use cmd to start some software
cmdtwo = "code D:\\"                     #[optional] ### Of course add some parameters is enabled
```

2. run init.exe in commandline or run(Win+R)

```shell   
init %para1% %para2% %para3%...
```

these parameter is what you have set in ```init.ini```. Example:

```
init test
>>>steam.exe VisualStudioCode pycharm started
```

## Program mermaid
```flow
      (start)
         |
         |
         ↓
  [init project]=>[| process cmd parameter && read .ini file]
         |
         ↓
<foreach: secitons>----(section==null)---->(Program Exit)
         | 
  (section!=null)
         ↓   
[process section 'AlterName', get AlterNameList<string>]
         |
         |
         ↓
 <AlterName==null?>----YES---->(Program Exit)
         |
         |NO
         ↓
[process AlterNameList<string> get targetExecuteCMD]
         |
         |
         ↓
   [execute cmd]
         |
         |
         ↓
      （END)


```
