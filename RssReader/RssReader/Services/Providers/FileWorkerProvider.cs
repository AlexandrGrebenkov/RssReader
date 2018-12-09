﻿using RssReader.Services.Mock;
using System;
using Xamarin.Forms;

namespace RssReader.Services.Providers
{
    /// <summary>
    /// Провайдер сервиса доступа к файлам
    /// Использует ленивую инициализацию и через DependencyService получает реализацию для конкретной платформы
    /// </summary>
    public static class FileWorkerProvider
    {
        static Lazy<IFileWorker> implementation = new Lazy<IFileWorker>(() => Create(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Доступ к текущей имплементации переферии
        /// </summary>
        public static IFileWorker Current
        {
            get
            {
                var ret = implementation.Value;
                if (ret == null)
                {
                    ret = new FileWorker_Mock();
                    //throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IFileWorker Create() => DependencyService.Get<IFileWorker>();

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("Этот сервис не реализован на данной платформе");
    }
}
