using System;
using System.IO;
using System.Xml.Serialization;

/// <summary>
/// XML序列化公共处理类
/// </summary>
public static class XmlBLL
{
    /// <summary>
    /// 将实体对象转换成XML
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="obj">实体对象</param>
    public static string XmlSerialize<T>(T obj)
    {
        try
        {
            using (StringWriter sw = new StringWriter())
            {
                Type t = obj.GetType();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj);
                sw.Close();
                return sw.ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("将实体对象转换成XML异常", ex);
        }
    }

    /// <summary>
    /// 将XML转换成实体对象
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="strXML">XML</param>
    public static T DESerializer<T>(string strXML) where T : class
    {
        try
        {
            using (StringReader sr = new StringReader(strXML))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(sr) as T;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("将XML转换成实体对象异常", ex);
        }
    }
}