using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace SObjectApplication.Repository.Serializer
{
	public static class Serializer
	{
		public static string ToBin(object formattedObject)
		{
			return new Serial().ToString(formattedObject);
		}

		public static T ToObj<T>(string binaryString)
		{
			return (T)(new Serial().ToObject<T>(binaryString));
		}
	}
	public class Serial
	{
		private class ArrayHelper
		{
			public Object[] list;
		}

		private static XmlDocument xml = new XmlDocument();
		public static int Id = 0;
		public static Dictionary<int, Object> relList = new Dictionary<int, object>();

		public int GetNewId()
		{
			return Id++;
		}

		public XmlNode ToBinary(Object obj, XmlNode root = null)
		{
			XmlNode rootNode;
			if (root == null)
			{
				relList.Add(Id++, obj);
				rootNode = xml.CreateElement("root");
				xml.AppendChild(rootNode);
			}
			else
				rootNode = root;

			if (obj == null)
			{
				XmlNode tmp = xml.CreateElement("NullValue");
				XmlAttribute typeAttribute = xml.CreateAttribute("Type");
				typeAttribute.Value = "null";
				tmp.Attributes.Append(typeAttribute);
				rootNode.AppendChild(tmp);
				return rootNode;
			}
			if (obj.GetType() == typeof(DateTime))
			{

				XmlNode tmp = xml.CreateElement("Value");
				if (obj == null)
					tmp.InnerText = "null";
				else
				{
					dynamic time = obj;
					string formatString = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";

					tmp.InnerText = time.ToString(formatString);
					;
				}

				XmlAttribute typeAttribute = xml.CreateAttribute("Type");

				typeAttribute.Value = obj.GetType().ToString();
				tmp.Attributes.Append(typeAttribute);
				rootNode.AppendChild(tmp);
				return rootNode;

			}
			if (obj.GetType().IsArray)
			{
				dynamic array = obj;
				ArrayHelper arHelp = new ArrayHelper();
				arHelp.list = new object[array.Length];
				for (int i = 0; i < array.Length; i++)
					arHelp.list[i] = (object)array[i];

				return new Serial().ToBinary(arHelp, rootNode);
			}

			if (obj != null && (obj.GetType().IsPrimitive || (obj is String)))
			{
				if (obj.GetType() == typeof(String) && obj == "")
				{
					XmlAttribute isEmptyStrAttribute = xml.CreateAttribute("IsEmptyStr");

					isEmptyStrAttribute.Value = "true";
					rootNode.Attributes.Append(isEmptyStrAttribute);
				}
				XmlNode tmp = xml.CreateElement("Value");
				XmlAttribute typeAttribute = xml.CreateAttribute("Type");
				typeAttribute.Value = obj.GetType().ToString();
				tmp.Attributes.Append(typeAttribute);
				tmp.InnerText = obj.ToString();
				rootNode.AppendChild(tmp);
				return rootNode;
			}



			foreach (
				FieldInfo item in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (item.FieldType == typeof(DateTime))
				{
					XmlNode tmp = xml.CreateElement(GetCleanFieldName(item));
					if (item.GetValue(obj) == null)
						tmp.InnerText = "null";
					else
					{
						dynamic time = item.GetValue(obj);
						string formatString = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";
						tmp.InnerText = time.ToString(formatString);
					}

					XmlAttribute typeAttribute = xml.CreateAttribute("Type");

					typeAttribute.Value = item.FieldType.ToString();
					tmp.Attributes.Append(typeAttribute);
					rootNode.AppendChild(tmp);
					continue;
				}
				if (item.FieldType.IsPrimitive || item.FieldType == typeof(String))
				{
					XmlNode tmp = xml.CreateElement(GetCleanFieldName(item));

					if (item.GetValue(obj) == null)
						tmp.InnerText = "null";
					else
					{

						tmp.InnerText = (String)Convert.ChangeType(item.GetValue(obj), TypeCode.String);
					}
					if (item.FieldType == typeof(String) && item.GetValue(obj) == "")
					{
						XmlAttribute isEmptyStrAttribute = xml.CreateAttribute("IsEmptyStr");

						isEmptyStrAttribute.Value = "true";
						tmp.Attributes.Append(isEmptyStrAttribute);
					}

					XmlAttribute typeAttribute = xml.CreateAttribute("Type");

					typeAttribute.Value = item.FieldType.ToString();
					tmp.Attributes.Append(typeAttribute);
					rootNode.AppendChild(tmp);
				}
				else if (IsReferenceType(item.FieldType))
				{
					XmlNode tmp = xml.CreateElement(GetCleanFieldName(item));
					if (item.GetValue(obj) == null)
						tmp.InnerText = "null";
					else
					{

						if (!relList.ContainsValue(item.GetValue(obj)))
						{
							relList.Add(Id, item.GetValue(obj));
							XmlAttribute idAttribute = xml.CreateAttribute("Id");
							idAttribute.Value = GetNewId().ToString();
							new Serial().ToBinary(item.GetValue(obj), tmp);

							tmp.Attributes.Append(idAttribute);

						}
						else
						{
							XmlNode toIdNode = xml.CreateElement("RefToId");
							toIdNode.InnerXml =
								relList.Where(x => (x.Value == item.GetValue(obj))).Select(x => (x.Key)).FirstOrDefault().ToString();
							tmp.AppendChild(toIdNode);
							XmlAttribute toIdAttribute = xml.CreateAttribute("ToId");
							XmlAttribute isToIdAttribute = xml.CreateAttribute("IsToId");
							isToIdAttribute.Value = "true";
							toIdAttribute.Value =
								relList.Where(x => (x.Value == item.GetValue(obj))).Select(x => (x.Key)).FirstOrDefault().ToString();
							tmp.Attributes.Append(toIdAttribute);
							tmp.Attributes.Append(isToIdAttribute);
							XmlAttribute idAttribute = xml.CreateAttribute("Id");
							idAttribute.Value = GetNewId().ToString();
							tmp.Attributes.Append(idAttribute);

						}





					}


					XmlAttribute typeAttribute = xml.CreateAttribute("Type");
					XmlAttribute refAttribute = xml.CreateAttribute("IsReference");
					refAttribute.Value = "true";
					typeAttribute.Value = item.FieldType.ToString();

					tmp.Attributes.Append(typeAttribute);
					tmp.Attributes.Append(refAttribute);

					rootNode.AppendChild(tmp);


				}
				else if (IsArray(item.FieldType))
				{
					XmlNode tmpNode = xml.CreateElement(GetCleanFieldName(item));
					if (item.GetValue(obj) == null)
						tmpNode.InnerText = "null";
					else
					{

						dynamic array = ((IList)item.GetValue(obj));
						XmlAttribute lenAttribute = xml.CreateAttribute("Len");
						dynamic tmparray = item.GetValue(obj);

						int Size = tmparray.Length;
						if (item.Name == "_items")
							Size--;
						lenAttribute.Value = Size.ToString();
						tmpNode.Attributes.Append(lenAttribute);
						for (int i = 0; i < Size; i++)
						{
							if (array[i] == null)
							{
								XmlNode tmp = xml.CreateElement("Value");
								tmp.InnerXml = "null";
								tmpNode.AppendChild(tmp);
							}
							if (array[i].GetType() == typeof(DateTime))
							{
								XmlNode tmp = xml.CreateElement("Value");
								if (array[i] == null)
									tmp.InnerText = "null";
								else
								{
									dynamic time = array[i];
									string formatString = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";
									tmp.InnerText = time.ToString(formatString);
								}

								XmlAttribute typeAttribute1 = xml.CreateAttribute("Type");

								typeAttribute1.Value = array[i].GetType().ToString();
								tmp.Attributes.Append(typeAttribute1);
								tmpNode.AppendChild(tmp);
								continue;
							}
							if ((array[i].GetType().IsPrimitive || array[i].GetType() == typeof(String)))
							{
								XmlNode tmp = xml.CreateElement("Value");
								if (array[i] == null)
									tmp.InnerText = "null";
								else
								{
									tmp.InnerText = (String)Convert.ChangeType(array[i], TypeCode.String);
								}

								XmlAttribute typeAttribute1 = xml.CreateAttribute("Type");

								typeAttribute1.Value = array[i].GetType().ToString();
								tmp.Attributes.Append(typeAttribute1);
								tmpNode.AppendChild(tmp);
							}
							else if (IsReferenceType(array[i].GetType()))
							{
								XmlNode tmp = xml.CreateElement("Value");
								if (array[i] == null)
									tmp.InnerText = "null";
								else
								{
									if (!relList.ContainsValue(array[i]))
									{
										relList.Add(Id, array[i]);
										XmlAttribute idAttribute = xml.CreateAttribute("Id");
										idAttribute.Value = GetNewId().ToString();
										new Serial().ToBinary(array[i], tmp);
										tmp.Attributes.Append(idAttribute);
									}
									else
									{
										XmlNode toIdNode = xml.CreateElement("RefToId");
										XmlAttribute idAttribute = xml.CreateAttribute("Id");
										idAttribute.Value = GetNewId().ToString();
										toIdNode.InnerXml = relList.Where(x => (x.Value == array[i])).Select(x => (x.Key)).FirstOrDefault().ToString();
										tmp.Attributes.Append(idAttribute);
										tmp.AppendChild(toIdNode);
										XmlAttribute toIdAttribute = xml.CreateAttribute("ToId");
										XmlAttribute isToIdAttribute = xml.CreateAttribute("IsToId");
										isToIdAttribute.Value = "true";
										toIdAttribute.Value = relList.Where(x => (x.Value == array[i])).Select(x => (x.Key)).FirstOrDefault().ToString();
										tmp.Attributes.Append(toIdAttribute);
										tmp.Attributes.Append(isToIdAttribute);

									}






								}


								XmlAttribute typeAttribute1 = xml.CreateAttribute("Type");
								XmlAttribute refAttribute1 = xml.CreateAttribute("IsReference");
								refAttribute1.Value = "true";
								typeAttribute1.Value = array[i].GetType().ToString();

								tmp.Attributes.Append(typeAttribute1);
								tmp.Attributes.Append(refAttribute1);

								tmpNode.AppendChild(tmp);
							}

						}
					}
					XmlAttribute typeAttribute = xml.CreateAttribute("Type");
					XmlAttribute arrayAttribute = xml.CreateAttribute("IsArray");
					arrayAttribute.Value = "true";
					typeAttribute.Value = item.FieldType.ToString();

					tmpNode.Attributes.Append(typeAttribute);
					tmpNode.Attributes.Append(arrayAttribute);

					rootNode.AppendChild(tmpNode);
				}
			}



			return rootNode;
		}


		public Object ToObjct<T>(String binaryString)
		{

			XmlReader reader_tmp;
			MethodInfo method;
			MethodInfo generic;

			XmlNode xmlDoc = xml.CreateElement("root");
			xmlDoc.InnerXml = binaryString;
			XmlReader reader = new XmlNodeReader(xmlDoc);
			reader.MoveToContent();
			reader.Read();



			if (reader.Value == "null")
				return null;
			if (typeof(T) == typeof(DateTime))
			{
				while (reader.Name == "Value" || reader.NodeType != XmlNodeType.Text)
				{
					reader.Read();
				}
				return
					Convert.ChangeType(
						DateTime.ParseExact(reader.Value, "yyyy-MM-ddTHH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture), typeof(T));
			}
			if (typeof(T).IsPrimitive || typeof(T) == typeof(String))
			{
				while (reader.NodeType != XmlNodeType.Text && reader.GetAttribute("IsEmptyStr") != "true")
				{
					reader.Read();
				}
				dynamic tmpVar = (T)Convert.ChangeType(reader.Value, typeof(T));

				if (typeof(T) == typeof(String) && tmpVar == "null")
					tmpVar = null;


				//reader.ReadToFollowing("Value");
				//reader.Read();
				return tmpVar;
			}
			if (typeof(T).IsArray)
			{
				while (reader.GetAttribute("IsArray") == null)
				{
					reader.Read();
				}
				int size = Convert.ToInt32(reader.GetAttribute("Len"));
				reader.Read();

				var array = Array.CreateInstance(typeof(T).GetElementType(), size);


				for (int i = 0; i < size; i++)
				{
					reader_tmp = reader.ReadSubtree();
					reader_tmp.MoveToContent();

					method = typeof(Serial).GetMethod("ToObjct");
					generic = method.MakeGenericMethod(Type.GetType(reader.GetAttribute("Type")));

					var tmpint = generic.Invoke(this, new object[] { reader_tmp.ReadOuterXml() });
					array.SetValue(tmpint, i);
					reader.Read();
				}
				Convert.ChangeType(array, typeof(T));
				return array;
			}
			T ReturnObject;

			ReturnObject = (T)Activator.CreateInstance(typeof(T));

			if (reader.Name == "RefToId" || reader.GetAttribute("IsToId") == "true")
			{
				if (reader.Name == "RefToId")
				{
					var tmp = relList[Convert.ToInt32(reader.Value)];

					reader.Skip();
					return tmp;
				}
				else if (reader.GetAttribute("IsToId") == "true")
				{
					var tmp = relList[Convert.ToInt32(reader.GetAttribute("ToId"))];
					reader.Skip();
					reader.Read();
					return tmp;
				}


			}

			if (Id == 0)
			{
				relList.Add(0, ReturnObject);
				Id = -1;
			}
			else
			{
				if (Convert.ToInt32(reader.GetAttribute("Id")) == 0)
					return null;
				if (!relList.ContainsKey(Convert.ToInt32(reader.GetAttribute("Id"))))
				{

					relList.Add(Convert.ToInt32(reader.GetAttribute("Id")), ReturnObject);
				}
			}



			FieldInfo[] fi =
				ReturnObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);


			for (int i = 0; i < fi.Length; i++)
			{
				while (reader.NodeType == XmlNodeType.EndElement || reader.Name != GetCleanFieldName(fi[i]))
				{
					reader.Read();
				}

				if (IsReferenceType(fi[i].FieldType) && fi[i].FieldType != typeof(DateTime))
				{
					bool RefFlag = false;
					bool ToIdFlag = false;
					int id;
					int toId;
					while (!(reader.GetAttribute("IsReference") == "true" ||
							 reader.GetAttribute("IsToId") == "true" ||
							 reader.Name == "RefToId" ||
							 reader.Value == "null") ||
						   reader.NodeType == XmlNodeType.EndElement)
					{
						reader.Read();
					}
					if (reader.Value == "null")
						return null;
					RefFlag = reader.Name == "RefToId";
					ToIdFlag = reader.GetAttribute("IsToId") == "true";
					id = Convert.ToInt32(reader.GetAttribute("Id"));
					toId = Convert.ToInt32(reader.GetAttribute("ToId"));


					method = typeof(Serial).GetMethod("ToObjct");
					generic = method.MakeGenericMethod(fi[i].FieldType);



					if (RefFlag)
					{
						var tmp = relList[Convert.ToInt32(reader.Value)];
						fi[i].SetValue(ReturnObject, tmp);
						reader.Skip();
						reader.Read();
					}
					else if (ToIdFlag)
					{
						var tmp = relList[toId];
						fi[i].SetValue(ReturnObject, tmp);
						reader.Skip();
					}
					else
					{
						reader_tmp = reader.ReadSubtree();
						reader_tmp.MoveToContent();


						var tmp = generic.Invoke(this, new object[] { reader_tmp.ReadOuterXml() });
						fi[i].SetValue(ReturnObject, tmp);
					}


					continue;


				}


				if (reader.Name != GetCleanFieldName(fi[i]))
					reader.ReadToFollowing(GetCleanFieldName(fi[i]));
				reader_tmp = reader.ReadSubtree();
				reader_tmp.MoveToContent();
				method = typeof(Serial).GetMethod("ToObjct");
				generic = method.MakeGenericMethod(fi[i].FieldType);

				var tmpint = generic.Invoke(this, new object[] { reader_tmp.ReadOuterXml() });
				fi[i].SetValue(ReturnObject, tmpint);
			}

			return ReturnObject;
		}
		public new String ToString(Object obj, XmlNode root = null)
		{
			Serial.relList = new Dictionary<int, object>();
			Serial.Id = 0;
			XmlNode retObj = new Serial().ToBinary(obj);
			Serial.relList = new Dictionary<int, object>();
			Serial.Id = 0;
			return retObj.InnerXml;
		}
		public Object ToObject<T>(String binaryString)
		{
			Serial.relList = new Dictionary<int, object>();
			Serial.Id = 0;
			Object ret = new Serial().ToObjct<T>(binaryString);
			Serial.relList = new Dictionary<int, object>();
			Serial.Id = 0;
			return ret;
		}

		private String GetCleanFieldName(FieldInfo item)
		{
			Match tmp_match = Regex.Match(item.Name, "(?<=<).*(?=>)");
			if (tmp_match.Value.Length > 0)
				return tmp_match.Value;
			return item.Name;
		}

		public bool IsReferenceType(Type item)
		{

			if (item.IsPrimitive || item == typeof(String) || item.IsArray)
				return false;
			return true;
		}

		public static bool IsPossibleToCreateInstanceOfType(Type type)
		{
			try
			{
				Activator.CreateInstance(type);

				return true;
			}
			catch (Exception)
			{
				return false;

			}
		}

		public bool IsArray(Type item)
		{
			return item.IsArray;
		}
	}
}
