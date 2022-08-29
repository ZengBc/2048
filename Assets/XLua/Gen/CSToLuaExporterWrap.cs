#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class CSToLuaExporterWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CSToLuaExporter);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 8, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateNum", _m_CreateNum_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SlideOperate", _m_SlideOperate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "MergeOperate", _m_MergeOperate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearNumArray", _m_ClearNumArray_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetButton", _m_GetButton_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindButton", _m_FindButton_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Detect", _m_Detect_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new CSToLuaExporter();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CSToLuaExporter constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateNum_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _posX = LuaAPI.xlua_tointeger(L, 1);
                    int _posY = LuaAPI.xlua_tointeger(L, 2);
                    int _value = LuaAPI.xlua_tointeger(L, 3);
                    
                    CSToLuaExporter.CreateNum( _posX, _posY, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SlideOperate_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _posX = LuaAPI.xlua_tointeger(L, 1);
                    int _posY = LuaAPI.xlua_tointeger(L, 2);
                    int _newPosX = LuaAPI.xlua_tointeger(L, 3);
                    int _newPosY = LuaAPI.xlua_tointeger(L, 4);
                    
                    CSToLuaExporter.SlideOperate( _posX, _posY, _newPosX, _newPosY );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MergeOperate_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _posX = LuaAPI.xlua_tointeger(L, 1);
                    int _posY = LuaAPI.xlua_tointeger(L, 2);
                    int _newPosX = LuaAPI.xlua_tointeger(L, 3);
                    int _newPosY = LuaAPI.xlua_tointeger(L, 4);
                    
                    CSToLuaExporter.MergeOperate( _posX, _posY, _newPosX, _newPosY );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearNumArray_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    CSToLuaExporter.ClearNumArray(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetButton_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _uid = LuaAPI.xlua_tointeger(L, 1);
                    
                        var gen_ret = CSToLuaExporter.GetButton( _uid );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindButton_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _nodename = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = CSToLuaExporter.FindButton( _nodename );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Detect_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = CSToLuaExporter.Detect(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
