using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
    public class ParsedResponse
    {
        public List<Respons> responses { get; set; }
    }
    public class Vertex
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class BoundingPoly
    {
        public List<Vertex> vertices { get; set; }
    }

    public class TextAnnotation
    {
        public string locale { get; set; }
        public string description { get; set; }
        public BoundingPoly boundingPoly { get; set; }
    }

    public class DetectedLanguage
    {
        public string languageCode { get; set; }
        public double confidence { get; set; }
    }

    public class Property
    {
        public List<DetectedLanguage> detectedLanguages { get; set; }
    }

    public class DetectedLanguage2
    {
        public string languageCode { get; set; }
        public double confidence { get; set; }
    }

    public class Property2
    {
        public List<DetectedLanguage2> detectedLanguages { get; set; }
    }

    public class Vertex2
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class BoundingBox
    {
        public List<Vertex2> vertices { get; set; }
    }

    public class DetectedLanguage3
    {
        public string languageCode { get; set; }
        public double confidence { get; set; }
    }

    public class Property3
    {
        public List<DetectedLanguage3> detectedLanguages { get; set; }
    }

    public class Vertex3
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class BoundingBox2
    {
        public List<Vertex3> vertices { get; set; }
    }

    public class DetectedLanguage4
    {
        public string languageCode { get; set; }
    }

    public class Property4
    {
        public List<DetectedLanguage4> detectedLanguages { get; set; }
    }

    public class Vertex4
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class BoundingBox3
    {
        public List<Vertex4> vertices { get; set; }
    }

    public class DetectedLanguage5
    {
        public string languageCode { get; set; }
    }

    public class DetectedBreak
    {
        public string type { get; set; }
    }

    public class Property5
    {
        public List<DetectedLanguage5> detectedLanguages { get; set; }
        public DetectedBreak detectedBreak { get; set; }
    }

    public class Vertex5
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class BoundingBox4
    {
        public List<Vertex5> vertices { get; set; }
    }

    public class Symbol
    {
        public Property5 property { get; set; }
        public BoundingBox4 boundingBox { get; set; }
        public string text { get; set; }
        public double confidence { get; set; }
    }

    public class Word
    {
        public Property4 property { get; set; }
        public BoundingBox3 boundingBox { get; set; }
        public List<Symbol> symbols { get; set; }
        public double confidence { get; set; }
    }

    public class Paragraph
    {
        public Property3 property { get; set; }
        public BoundingBox2 boundingBox { get; set; }
        public List<Word> words { get; set; }
        public double confidence { get; set; }
    }

    public class Block
    {
        public Property2 property { get; set; }
        public BoundingBox boundingBox { get; set; }
        public List<Paragraph> paragraphs { get; set; }
        public string blockType { get; set; }
        public double confidence { get; set; }
    }

    public class Page
    {
        public Property property { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public List<Block> blocks { get; set; }
    }

    public class FullTextAnnotation
    {
        public List<Page> pages { get; set; }
        public string text { get; set; }
    }

    public class Respons
    {
        public List<TextAnnotation> textAnnotations { get; set; }
        public FullTextAnnotation fullTextAnnotation { get; set; }
    }

   

}
