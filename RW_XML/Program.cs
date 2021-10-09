using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RW_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            crearDocmento();
            // o
              //XmlTextWriter writer; 
              //writer = new XmlTextWriter("write.xml",Encoding.UTF8);
              //escribirDocmento(writer); 
            //leerXMLReader();
            leerXML();
        }

        private static void leerXML()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("miXML.xml");
                XmlNodeList empleados = xDoc.GetElementsByTagName("empleados");
                XmlNodeList listaEmpleados;
                {
                    XmlNodeList listado = ((XmlElement) empleados[0]).GetElementsByTagName("listado");
                    listaEmpleados = ((XmlElement)listado[0]).GetElementsByTagName("empleado");
                }
                if(listaEmpleados != null)
                {
                    foreach(XmlElement empleado in listaEmpleados)
                    {
                        XmlElement id = ((XmlElement) empleado.GetElementsByTagName("id")[0]);
                        XmlElement nombreCompleto = ((XmlElement) empleado.GetElementsByTagName("nombreCompleto")[0]);
                        XmlElement cuil = ((XmlElement) empleado.GetElementsByTagName("cuil")[0]);
                        XmlElement sector = ((XmlElement) empleado.GetElementsByTagName("sector")[0]);
                        XmlElement cupoAsignado = ((XmlElement) empleado.GetElementsByTagName("cuponAsignado")[0]);
                        XmlElement cuponConsumido = ((XmlElement) empleado.GetElementsByTagName("cuponConsumido")[0]);
                        Console.WriteLine("id: " + id.FirstChild.Value.Trim());
                        Console.WriteLine("nombreCompleto: " + nombreCompleto.FirstChild.Value.Trim());
                        Console.WriteLine("cuil: " + cuil.FirstChild.Value.Trim());
                        Console.WriteLine("sector: ");
                        Console.WriteLine(" |-[denominacion]: " + sector.GetAttribute("denominacion"));
                        Console.WriteLine(" |-[id]: " + sector.GetAttribute("id"));
                        Console.WriteLine(" |-[valorSemaforo]: " + sector.GetAttribute("valorSemaforo"));
                        Console.WriteLine(" |-[colorSemaforo]: " + sector.GetAttribute("colorSemaforo"));
                        Console.WriteLine("cuponAsignado: " + cupoAsignado.FirstChild.Value.Trim());
                        Console.WriteLine("cuponConsumido: " + cuponConsumido.FirstChild.Value.Trim());
                    }
                }
                XmlElement SubSectores = (XmlElement)((XmlElement)empleados[0]).GetElementsByTagName("subsectores")[0];
                XmlElement totalCupoAsignadoSector = (XmlElement)((XmlElement)empleados[0]).GetElementsByTagName("totalCupoAsignadoSector")[0];
                XmlElement totalCupoConsumidoSector = (XmlElement)((XmlElement)empleados[0]).GetElementsByTagName("totalCupoConsumidoSector")[0];
                XmlElement valorDial = (XmlElement)((XmlElement)empleados[0]).GetElementsByTagName("valorDial")[0];

                Console.WriteLine("SubSectores: " + SubSectores.InnerText);
                Console.WriteLine("totalCupoAsignadoSector: " + totalCupoAsignadoSector.InnerText);
                Console.WriteLine("totalCupoConsumidoSector: " + totalCupoConsumidoSector.InnerText);
                Console.WriteLine("valorDial: " + valorDial.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("no se ha podido leer el documento");
            }
        }

        private static void leerXMLReader()
        {
            String path = Directory.GetCurrentDirectory().Replace("bin\\Debug","");
            using (XmlReader reader = XmlReader.Create("miXML.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "empleado":
                                leerEmpleado(reader);
                                break;
                            case "subsectores":
                                reader.Read();
                                Console.WriteLine("subsectores: ");
                                break;
                            case "totalCupoAsignadoSector":
                                reader.Read();
                                Console.WriteLine("totalCupoAsignadoSector: ");
                                break;
                            case "totalCupoConsumidoSector":
                                reader.Read();
                                Console.WriteLine("totalCupoConsumidoSector: ");
                                break;
                        }
                    }
                }
                reader.Close();
            }
        }

        private static void leerEmpleado(XmlReader reader)
        {
            while (reader.Read())
            {
                if(reader.IsStartElement())
                    switch (reader.Name)
                    {
                        case "id":
                            reader.Read();
                            Console.WriteLine("id: " + reader.Value.Trim());
                            break;
                        case "nombreCompleto":
                            reader.Read();
                            Console.WriteLine("nombreCompleto: " + reader.Value.Trim());
                            break;
                        case "cuil":
                            reader.Read();
                            Console.WriteLine("cuil: " + reader.Value.Trim());
                            break;
                        case "sector":
                            String denominacion = reader["denominacion"];
                            String id = reader["id"];
                            String valorSemaforo = reader["valorSemaforo"];
                            String colorSemaforo = reader["colorSemaforo"];
                            Console.WriteLine("sector" + 
                                "\n |-Atributo[denominacion]: " + denominacion +
                                "\n |-Atributo[id]: " + id +
                                "\n |-Atributo[valorSemaforo]: " + valorSemaforo +
                                "\n |-Atributo[colorSemaforo]: " + colorSemaforo );
                            break;
                        case "cuponAsignado":
                            reader.Read();
                            Console.WriteLine("cuponAsignado: " + reader.Value.Trim());
                            break;
                        case "cuponConsumido":
                            reader.Read();
                            Console.WriteLine("cuponConsumido: " + reader.Value.Trim());
                            return;
                            break;
                    }
            }
        }

        private static void escribirDocmento(XmlTextWriter writer)
        {
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("empleados");

            writer.WriteStartElement("listado");
            //agregamos empleado
            writer.WriteStartElement("empleado");
            writer.WriteElementString("id","4884");
            writer.WriteElementString("nombreCompleto","Rodriguez, Victor");
            writer.WriteElementString("cuil", "20103180326");

            writer.WriteStartElement("sector");
            writer.WriteAttributeString("denominacion","Gerencia Operativa");
            writer.WriteAttributeString("id","137");
            writer.WriteAttributeString("valorSemaforo","Gerencia Operativa");
            writer.WriteAttributeString("colorSemaforo", "VERDE");
            writer.WriteEndElement();

            writer.WriteElementString("cuponAsignado", "1837.17");
            writer.WriteElementString("cuponConsumido", "658.02");
            writer.WriteEndElement();
            //terminamos de agregarEmpleado
            //agregamos un empleado
            writer.WriteStartElement("empleado");
            writer.WriteElementString("id", "1225");
            writer.WriteElementString("nombreCompleto", "Rodriguez, Victor");
            writer.WriteElementString("cuil", "20271265817");

            writer.WriteStartElement("sector");
            writer.WriteAttributeString("denominacion", "Gerencia Operativa");
            writer.WriteAttributeString("id", "44");
            writer.WriteAttributeString("valorSemaforo", "130.13");
            writer.WriteAttributeString("colorSemaforo", "ROJO");
            writer.WriteEndElement();

            writer.WriteElementString("cuponAsignado", "1837.17");
            writer.WriteElementString("cuponConsumido", "625.46");
            writer.WriteEndElement();
            writer.WriteEndElement();
            
            writer.WriteElementString("subsectores", "5");
            writer.WriteElementString("totalCupoAsignadoSector", "5217.21");
            writer.WriteElementString("totalCupoAsignadoSector", "1405.88");
            writer.WriteElementString("valorDial", "33.34");
            
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private static void crearDocmento()
        {
            XDocument miXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Lista Empleados"),
                new XElement("empleados",
                    new XElement("listado",
                        new XElement("empleado",
                            new XElement("id", "4884"),
                            new XElement("nombreCompleto", "Rodriguez, Victor"),
                            new XElement("cuil", "20103180326"),
                            new XElement("sector",
                                new XAttribute("denominacion","Gerencia Operativa"),
                                new XAttribute("id", "137"),
                                new XAttribute("valorSemaforo", "130.13"),
                                new XAttribute("colorSemaforo", "VERDE"),""),
                            new XElement("cuponAsignado", "1837.17"),
                            new XElement("cuponConsumido", "658.02")
                        ),
                        new XElement("empleado",
                            new XElement("id", "1225"),
                            new XElement("nombreCompleto", "Sanchez, Juan Ignacio"),
                            new XElement("cuil", "20271265817"),
                            new XElement("sector",
                                new XAttribute("denominacion", "Gerencia Operativa"),
                                new XAttribute("id", "44"),
                                new XAttribute("valorSemaforo", "130.13"),
                                new XAttribute("colorSemaforo", "ROJO"),""),
                            new XElement("cuponAsignado", "1837.17"),
                            new XElement("cuponConsumido", "625.46")
                        )
                    ),
                    new XElement("subsectores", "5"),
                    new XElement("totalCupoAsignadoSector", "4217.21"),
                    new XElement("totalCupoConsumidoSector", "4217.21"),
                    new XElement("valorDial", "33.34")
                )
           );
           miXML.Save(@"miXML.xml");
        }
    }
}
