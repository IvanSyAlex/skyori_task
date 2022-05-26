using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestSkyori
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenManager
    {
        private ScreenTypes _screenState = ScreenTypes.Menu;
        private DataForChecks _dataForCheck;

        /// <summary>
        /// Запустить экран взаимодействия с пользователем
        /// </summary>
        /// <returns></returns>
        public DataForChecks Start()
        {
            _dataForCheck = new DataForChecks();

            while (IsQuit())
            {
                Console.Write(PrepareScreen());
                SetScreenState(Console.ReadKey().Key);
                InputValue();
                SetScreenState(ConsoleKey.M);
            }

            return _dataForCheck;
        }

        /// <summary>
        /// Проверка на завершения ввода данных с консоли
        /// </summary>
        /// <returns></returns>
        private bool IsQuit()
        {
            if (_dataForCheck == null)
                return false;

            if (_dataForCheck.ScreenResult == ScreenTypes.AppQuit)
                return false;

            if (filledRequiredData()
                && _dataForCheck.ScreenResult == ScreenTypes.StartCheck)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Определить результат выбора пользователя
        /// </summary>
        /// <param name="key"></param>
        private void SetScreenState(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.S:
                    _screenState = ScreenTypes.EnterSite;
                    break;
                case ConsoleKey.D:
                    _screenState = ScreenTypes.EnterConnDb;
                    break;
                case ConsoleKey.E:
                    _screenState = ScreenTypes.EnterEmail;
                    break;
                case ConsoleKey.Q:
                    _dataForCheck.ScreenResult = ScreenTypes.AppQuit;
                    break;
                case ConsoleKey.Y:
                    _dataForCheck.ScreenResult = ScreenTypes.StartCheck;
                    break;
                default:
                    _screenState = ScreenTypes.Menu;
                    break;
            }
        }

        /// <summary>
        /// Подготовить текст для экрана консоли
        /// </summary>
        /// <returns></returns>
        private string PrepareScreen()
        {
            Console.Clear();
            var result = "";

            if (_dataForCheck == null)
                _dataForCheck = new DataForChecks();

            if (_screenState == ScreenTypes.Menu)
                result += PrepareMenuText();

            return result;
        }

        /// <summary>
        /// Обработать ввод данных с консоли
        /// </summary>
        private void InputValue()
        {
            switch (_screenState)
            {
                // ввод email
                case ScreenTypes.EnterEmail:
                {
                    Console.Clear();
                    Console.WriteLine(ScreenTexts.ValueInputString.Replace("&", ScreenTexts.ExplanationPartEmail));

                    var result = Console.ReadLine();
                    if (!string.IsNullOrEmpty(result))
                        _dataForCheck.Email = result;

                    return;
                }
                // ввод адреса сайта
                case ScreenTypes.EnterSite:
                {
                    Console.Clear();
                    Console.WriteLine(ScreenTexts.ValueInputString.Replace("&", ScreenTexts.ExplanationPartSite));

                    var result = Console.ReadLine();
                    if (!string.IsNullOrEmpty(result))
                        _dataForCheck.SiteList.Add(result);

                    return;
                }
                // ввод строки подключения
                case ScreenTypes.EnterConnDb:
                {
                    Console.Clear();
                    Console.WriteLine(ScreenTexts.ValueInputString.Replace("&", ScreenTexts.ExplanationPartDbStr));

                    var result = Console.ReadLine();
                    if (!string.IsNullOrEmpty(result))
                        _dataForCheck.ConnectionDbStringList.Add(result);

                    break;
                }
            }
        }

        /// <summary>
        /// Подготовить текст "Меню"
        /// </summary>
        /// <returns></returns>
        private string PrepareMenuText()
        {
            var result = string.Empty;

            // текст с пояснениями
            result += PrepareExplanationText();

            // строка с адресом сайте
            result += _dataForCheck.SiteList.Count <= 0 
                ? ScreenTexts.SelectString 
                : ScreenTexts.AppendString;
            result += $" {ScreenTexts.SiteOutputString}\n";

            // строка со строкой подключения к БД
            result += _dataForCheck.ConnectionDbStringList.Count <= 0
                ? ScreenTexts.SelectString
                : ScreenTexts.AppendString;
            result += $" {ScreenTexts.DataBaseOutputString}\n";

            // строка с email
            result += string.IsNullOrEmpty(_dataForCheck.Email)
                ? ScreenTexts.SelectString
                : ScreenTexts.ChangeString;
            result += $" {ScreenTexts.EmailOutputString}\n";

            // строка с завершением приложения
            result += $"\n\n{ScreenTexts.AppQuitString}\n";

            // строка с запуском проверок
            if (filledRequiredData())
                result += $"{ScreenTexts.StartCheckString}\n";

            // строка уже введенными данными
            result += PrepareCurrentValuesText();

            return result;
        }

        /// <summary>
        /// Подготовить текст с пояснениями
        /// </summary>
        /// <param name="screenText"></param>
        /// <returns></returns>
        private string PrepareExplanationText()
        {
            var result = string.Empty;

            if (filledRequiredData())
                return result;

            // строка с пояснениями
            result += $"--------------------------------------------------------------\n";
            result += $"{ScreenTexts.Explanation}\n";
            if (_dataForCheck.SiteList.Count <= 0)
                result += $"- {ScreenTexts.ExplanationPartEmail}\n";
            if (_dataForCheck.ConnectionDbStringList.Count <= 0)
                result += $"- {ScreenTexts.ExplanationPartDbStr}\n";
            if (string.IsNullOrEmpty(_dataForCheck.Email))
                result += $"- {ScreenTexts.ExplanationPartSite}\n";
            result += $"--------------------------------------------------------------\n\n";

            return result;
        }

        /// <summary>
        /// Подготовить текст с уже введенными данными
        /// </summary>
        /// <returns></returns>
        private string PrepareCurrentValuesText()
        {
            var result = string.Empty;

            // строка с пояснениями
            result += $"\n\n===============================================================\n";
            result += $"{ScreenTexts.CurrentDataString}\n";

            // указанныe сайты
            result += $"{ScreenTexts.CurrentDataSitesString}\n";
            result += $"{GetValuesString(_dataForCheck.SiteList)}\n";

            // указанныe строки БД
            result += $"{ScreenTexts.CurrentDataConnDbString}\n";
            result += $"{GetValuesString(_dataForCheck.ConnectionDbStringList)}\n";

            // указанный email
            result += $"{ScreenTexts.CurrentDataEmailString}\n";
            result += $"{GetValuesString(new List<string> { _dataForCheck.Email })}\n";
            result += $"===============================================================\n\n";

            return result;
        }

        /// <summary>
        /// Сформировать строку с указанными сайтами или строками БД
        /// </summary>
        /// <param name="values"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        private string GetValuesString(List<string> values, int maxLength = 100)
        {
            var result = string.Join(' ', values);

            if (result.Length > maxLength)
                return result.Substring(0, maxLength - 4) + "...";

            return result;
        }

        /// <summary>
        /// Проверка на заполненность минимально необходимых данных
        /// для запуска проверок
        /// </summary>
        /// <returns></returns>
        private bool filledRequiredData()
        {
            return _dataForCheck.SiteList.Count > 0
                   && _dataForCheck.ConnectionDbStringList.Count > 0
                   && !string.IsNullOrEmpty(_dataForCheck.Email);
        }
    }
}