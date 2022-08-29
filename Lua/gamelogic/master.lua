--------------------------------------------------------------------
slave=require("gamelogic.slave")
--------------------------------------------------------------------
SMGLuaExporter=CS.UnityEngine.SceneManagement.SceneManager
--------------------------------------------------------------------
master={}
isFirst=true
isCreate=false
numArray={}
numArray[0]={}
numArray[1]={}
numArray[2]={}
numArray[3]={}
--------------------------------------------------------------------
local function isNil(x,y)
    if(x<0 or x>3 or y<0 or y>3)
    then
        return false
    elseif(numArray[x][y]~=null)
    then
        return false
    end
    return true
end
--------------------------------------------------------------------
local function checkOverFlow()
    --局部变量
    local quantity
    local i
    local j
    --逻辑
    quantity=0
    for i=0,3,1 do
        for j=0,3,1 do
            if(numArray[i][j]==nil)
            then
                return false;
            else
                quantity=quantity+1
            end
        end
    end
    if(quantity==16)
    then
        return true
    end
end
--------------------------------------------------------------------
local function UpdateNumArray(posX,posY,value)
    --局部变量
    local Num
    --逻辑
    --生成lua数字数据结构
    Num={
        val=value,
        isOneMove=false,
        Output=function()
            print(val)
        end
    }
    numArray[posY][posX]=Num
end
--------------------------------------------------------------------
local function NumUnLock()
    --局部变量
    local i
    local j
    local hashOnce
    --逻辑
    for i=0,3,1 do
        for j=0,3,1 do
            hashOnce=numArray[i]
            if(hashOnce[j]~=nil)
            then
                hashOnce[j].isOneMove=false
            end
        end
    end
end
--------------------------------------------------------------------
local function Move(i,j,input)
    --局部变量
    local index
    local temp
    local hashOnce=numArray[i]
    local hashOnceMinus
    local hashOnceAdd
    --逻辑
    if(input==1)
    then
        -- print("左")
        index=1
        while(isNil(i,j-index))
        do
            index=index+1
        end
        if(index>1)
        then
            temp=hashOnce[j]
            hashOnce[j]=nil
            j=j-index+1
            hashOnce[j]=temp
            isCreate=true
        end
        if(j>0 and hashOnce[j].val==hashOnce[j-1].val and hashOnce[j-1].isOneMove==false)
        then
            hashOnce[j-1].isOneMove=true
            hashOnce[j]=nil
            hashOnce[j-1].val=2*hashOnce[j-1].val
            isCreate=true
            slave.MergeOperate(j+index-1,i,j-1,i)
        else
            slave.SlideOperate(j+index-1,i,j,i)
        end
        --UpdateCanvas()
    elseif(input==2)
    then
        -- print("右")
        index=1
        while(isNil(i,j+index))
        do
            index=index+1
        end
        if(index>1)
        then
            temp=hashOnce[j]
            hashOnce[j]=nil
            j=j+index-1
            hashOnce[j]=temp
            isCreate=true
        end
        if(j<3 and hashOnce[j].val==hashOnce[j+1].val and hashOnce[j+1].isOneMove==false)
        then
            hashOnce[j+1].isOneMove=true
            hashOnce[j]=nil
            hashOnce[j+1].val=2*hashOnce[j+1].val
            isCreate=true
            slave.MergeOperate(j-index+1,i,j+1,i)
        else
            slave.SlideOperate(j-index+1,i,j,i)
        end
        --UpdateCanvas()
    elseif(input==3)
    then
        -- print("上")
        index=1
        while(isNil(i-index,j))
        do
            index=index+1
        end
        if(index>1)
        then
            temp=hashOnce[j]
            hashOnce[j]=nil
            i=i-index+1
            hashOnce=numArray[i]
            hashOnce[j]=temp
            isCreate=true
        end
        hashOnceMinus=numArray[i-1]
        if(i>0 and hashOnce[j].val==hashOnceMinus[j].val and hashOnceMinus[j].isOneMove==false)
        then
            hashOnceMinus[j].isOneMove=true
            hashOnce[j]=nil
            hashOnceMinus[j].val=2*hashOnceMinus[j].val
            isCreate=true
            slave.MergeOperate(j,i+index-1,j,i-1)
        else
            slave.SlideOperate(j,i+index-1,j,i)
        end
        --UpdateCanvas()
    elseif(input==4)
    then
        -- print("下")
        index=1
        while(isNil(i+index,j))
        do
            index=index+1
        end
        if(index>1)
        then
            temp=hashOnce[j]
            hashOnce[j]=nil
            i=i+index-1
            hashOnce=numArray[i]
            hashOnce[j]=temp
            isCreate=true
        end
        hashOnceAdd=numArray[i+1]
        if(i<3 and hashOnce[j].val==hashOnceAdd[j].val and hashOnceAdd[j].isOneMove==false)
        then
            hashOnceAdd[j].isOneMove=true
            hashOnce[j]=nil
            hashOnceAdd[j].val=2*hashOnceAdd[j].val
            isCreate=true
            slave.MergeOperate(j,i-index+1,j,i+1)
        else
            slave.SlideOperate(j,i-index+1,j,i)
        end
        --UpdateCanvas()
    end
end
--------------------------------------------------------------------
local function MoveNum(input)
    --局部变量
    local i
    local j
    --逻辑
    if(input==1)
    then
        -- print("左")
        for i=0,3,1 do
            for j=1,3,1 do
                if(numArray[i][j]~=nil)
                then
                    Move(i,j,input)
                end
            end
        end
    elseif(input==2)
    then
        -- print("右")
        for i=0,3,1 do
            for j=2,0,-1 do
                if(numArray[i][j]~=nil)
                then
                    Move(i,j,input)
                end
            end
        end
    elseif(input==3)
    then
        -- print("上")
        for j=0,3,1 do
            for i=1,3,1 do
                if(numArray[i][j]~=nil)
                then
                    Move(i,j,input)
                end
            end
        end
    elseif(input==4)
    then
        -- print("下")
        for j=0,3,1 do
            for i=2,0,-1 do
                if(numArray[i][j]~=nil)
                then
                    Move(i,j,input)
                end
            end
        end
    end
    NumUnLock()
end
--------------------------------------------------------------------
local function CreateNumber()
    --局部变量
    local value
    local posX
    local posY
    local currentNum
    --逻辑
    value=math.random()
    --检查lua数字数组是否溢出
    if(checkOverFlow())
    then
        return nil
    end
    --随机选择2或4
    if(value>0.2)
    then
        value=2
    else
        value=4 
    end
    repeat
    --随机生成位置坐标
        posX=math.floor(math.random(0,3))
        posY=math.floor(math.random(0,3))
        --当前lua数字数组的值
        currentNum=numArray[posY][posX]
    until(currentNum==nil)
    slave.CreateNum(posX,posY,value)
    UpdateNumArray(posX,posY,value)
end
--------------------------------------------------------------------
local function SecondOperate(input)
    --移动数字
    MoveNum(input)
    --随机生成2或4
    if(isCreate==true)
    then
        isCreate=false
        CreateNumber()
    end
end
--------------------------------------------------------------------
local function FirstOperate()
    --随机生成2或4
    CreateNumber()
    --随机生成2或4
    CreateNumber()
end
--------------------------------------------------------------------
local function DetectAndOperate()
    --局部变量
    local input
    --用户输入
    --1 ← 2 →
    --3 ↑ 4 ↓
    input=slave.Detect()
    if(input~=0)
    then
        SecondOperate(input)
    end
end
--------------------------------------------------------------------
function master.operate()
    local SceneName=SMGLuaExporter.GetActiveScene().name
    if(SceneName=="Scene2")
    then
        if(isFirst==true)
        then
            isFirst=false
            FirstOperate()  --第一次操作
        else
            DetectAndOperate()  --检测输入和第二次操作
        end
    end
end
--------------------------------------------------------------------
return master
--------------------------------------------------------------------