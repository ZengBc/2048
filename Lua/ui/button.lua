--------------------------------------------------------------------
CSToLuaExporter = CS.CSToLuaExporter
SMGLuaExporter=CS.UnityEngine.SceneManagement.SceneManager
--------------------------------------------------------------------
local button={}
button.StartBtn=-1     
button.BackBtn=-1     
button.RestartBtn=-1  
--------------------------------------------------------------------
local function ClearNumArray()
    CSToLuaExporter.ClearNumArray()
end
--------------------------------------------------------------------
local function ButtonRegist()
    StartBtn=CSToLuaExporter.FindButton("start")
    BackBtn=CSToLuaExporter.FindButton("back")
    RestartBtn=CSToLuaExporter.FindButton("restart")
end
--------------------------------------------------------------------
local function ButtonListen()
    if(StartBtn~=-1)
    then
        Button=CSToLuaExporter.GetButton(StartBtn)
        Button.onClick:AddListener(
            function()
                print("START")
                ClearNumArray()
                SMGLuaExporter.LoadScene("Scene2")
            end)
    end
    if(BackBtn~=-1)
    then
        Button=CSToLuaExporter.GetButton(BackBtn)
        Button.onClick:AddListener(
            function()
                print("BACK")
                ClearNumArray()
                SMGLuaExporter.LoadScene("Scene1")
            end)
    end
    if(RestartBtn~=-1)
    then
        Button=CSToLuaExporter.GetButton(RestartBtn)
        Button.onClick:AddListener(
            function()
                print("RESTART")
                ClearNumArray()
                SMGLuaExporter.LoadScene("Scene2")
            end)
    end
end
--------------------------------------------------------------------
function button.init()
    ButtonRegist()
    ButtonListen()
end
--------------------------------------------------------------------
return button
--------------------------------------------------------------------