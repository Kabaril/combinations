using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace Combinations.UI
{
    public class ItemWikiState : UIState
    {
        public DraggableUIPanel uIPanel;
        private List<UIElement> elements;
        private string rawText;
        private Vector2 totalContentSize;

        public ItemWikiState(string rawText)
        {
            this.rawText = rawText;
            compileText(rawText);
            var closeButton = new CloseButton(CloseButton.Texture);
            closeButton.OnClick += CloseButton_OnClick;
            closeButton.Activate();
            elements.Add(closeButton);
        }

        private void CloseButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            CombinationsModSystem.Instance.UnloadWiki();
        }

        private void compileText(string text)
        {
            elements = new List<UIElement>();
            if(text is null)
            {
                return;
            }
            string[] lines = text.Split('\n', options: StringSplitOptions.TrimEntries);

            float padding_top = 10f;
            float padding_left = 10f;

            float total_height = padding_top;
            float total_width = padding_left;
            foreach (string line in lines)
            {
                string line_text = line;
                var _scale = getScaleFromString(line_text);
                line_text = _scale.Item1.Trim();
                var _color = getColorFromString(line_text);
                line_text = _color.Item1.Trim();
                var _textures = getTexturesFromString(line_text, padding_left + 5f, total_height, _color.Item2, _scale.Item2);
                line_text = _textures.Item1.TrimEnd();
                elements.AddRange(_textures.Item2);
                Vector2 size = FontAssets.MouseText.Value.MeasureString(line_text);
                size *= _scale.Item2;
                TextElementUI _1 = new TextElementUI(line_text, new Vector2(padding_left, total_height), _color.Item2, _scale.Item2);
                _1.Height.Set(size.Y, 0);
                _1.Width.Set(size.X, 0);
                elements.Add(_1);
                total_height += size.Y;
                if(size.X > total_width)
                {
                    total_width = size.X;
                }
            }
            totalContentSize = new Vector2(total_width + (padding_left * 2f), total_height + padding_top);
        }

        string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        //Should be last in processing chain
        //![PATH/TO/TEXTURE]
        private Tuple<string, List<InlineTextureUI>> getTexturesFromString(string line, float offset_left, float top, Color color, float scale)
        {
            List<InlineTextureUI> textures = new List<InlineTextureUI>();
            MatchCollection matches = Regex.Matches(line, @"\!\[([^[]+)\]");
            foreach(Match match in matches)
            {
                int index = line.IndexOf(match.Value);
                float left = FontAssets.MouseText.Value.MeasureString(line.Substring(0, index)).X * scale;
                string texture_str = Regex.Match(match.Value, @"\!\[(.+)\]$").Groups[1].Value.Trim();
                InlineTextureUI texture = new InlineTextureUI(texture_str, color, left + offset_left, top, scale);
                textures.Add(texture);

                float replace_char_width = FontAssets.MouseText.Value.MeasureString(" ").X * scale;

                int count = (int)Math.Ceiling((texture.getWidth() + (offset_left / 2f)) / replace_char_width);

                line = ReplaceFirst(line, match.Value, new string(' ', count));
            }

            return new Tuple<string, List<InlineTextureUI>>(line, textures);
        }

        private Tuple<string, Color> getColorFromString(string line)
        {
            if(!line.StartsWith("~"))
            {
                return new Tuple<string, Color>(line, Color.White);
            }

            if (line.StartsWith("~0"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Red);
            }

            if (line.StartsWith("~1"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Green);
            }

            if (line.StartsWith("~2"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Blue);
            }

            if (line.StartsWith("~3"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Black);
            }

            if (line.StartsWith("~4"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Yellow);
            }

            if (line.StartsWith("~5"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Violet);
            }

            if (line.StartsWith("~6"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Orange);
            }

            if (line.StartsWith("~7"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Cyan);
            }

            if (line.StartsWith("~8"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Brown);
            }

            if (line.StartsWith("~9"))
            {
                return new Tuple<string, Color>(line.Substring(2), Color.Pink);
            }

            return new Tuple<string, Color>(line, Color.White);
        }

        private Tuple<string, float> getScaleFromString(string line)
        {
            if(line.StartsWith("####"))
            {
                return new Tuple<string, float>(line.Substring(4), 1.125f);
            }
            if(line.StartsWith("###"))
            {
                return new Tuple<string, float>(line.Substring(3), 1.5f);
            }
            if (line.StartsWith("##"))
            {
                return new Tuple<string, float>(line.Substring(2), 2f);
            }
            if (line.StartsWith("#"))
            {
                return new Tuple<string, float>(line.Substring(1), 2.5f);
            }
            return new Tuple<string, float>(line, 1f);
        }

        public override void OnInitialize()
        {
            uIPanel = new DraggableUIPanel();
            uIPanel.SetPadding(0);
            uIPanel.Left.Set(Main.screenWidth * 0.62f, 0f);
            uIPanel.Top.Set(Main.screenHeight * 0.1f, 0f);
            uIPanel.Width.Set(totalContentSize.X, 0f);
            uIPanel.Height.Set(totalContentSize.Y, 0f);
            uIPanel.BackgroundColor = new Color(73, 94, 171);

            foreach(UIElement element in elements)
            {
                uIPanel.Append(element);
            }

            Append(uIPanel);
        }
    }
}
