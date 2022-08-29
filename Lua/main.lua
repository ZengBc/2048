--------------------------------------------------------------------
local ui = require("ui.button")
local gamelogic = require("gamelogic.master")
--------------------------------------------------------------------
CSToLuaExporter = CS.CSToLuaExporter
SMGLuaExporter=CS.UnityEngine.SceneManagement.SceneManager
--------------------------------------------------------------------
function __enter__(param)
    ui.init()
end
--------------------------------------------------------------------
function __update__(deltaTime)
    gamelogic.operate()
end
--------------------------------------------------------------------