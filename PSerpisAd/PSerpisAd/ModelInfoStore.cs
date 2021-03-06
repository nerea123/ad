using System;
using System.Collections.Generic;

namespace Serpis.Ad
{
	public static class ModelInfoStore
	{
		private static Dictionary<Type,ModelInfo> modelInfos=new Dictionary<Type,ModelInfo>();


		public static ModelInfo Get(Type type){
		
			if (!modelInfos.ContainsKey (type))
				modelInfos [type] = new ModelInfo (type);
			
				return modelInfos [type];


		}

		public static ModelInfo Get(Type type,string id, string[]fields){

			if (!modelInfos.ContainsKey (type))
				modelInfos [type] = new ModelInfo (type,id,fields);

			return modelInfos [type];


		}
	}
}

