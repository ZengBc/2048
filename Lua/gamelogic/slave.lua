--------------------------------------------------------------------
CSToLuaExporter = CS.CSToLuaExporter
--------------------------------------------------------------------
slave={}
function slave.SlideOperate(posX,posY,newPosX,newPosY)
    CSToLuaExporter.SlideOperate(posX,posY,newPosX,newPosY)
end
--------------------------------------------------------------------
function slave.MergeOperate(posX,posY,newPosX,newPosY)
    CSToLuaExporter.MergeOperate(posX,posY,newPosX,newPosY)
end
--------------------------------------------------------------------
function slave.CreateNum(posX,posY,value)
    CSToLuaExporter.CreateNum(posX,posY,value)
end
--------------------------------------------------------------------
function slave.Detect()
	return CSToLuaExporter.Detect()
end
--------------------------------------------------------------------
return slave
--------------------------------------------------------------------