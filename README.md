# ExArchiver
Программа запускается, читает настройки из файла settings.json из папки, проверяет аргументы запуска программы.

Если есть аргументы и первый аргумент валиден, то в настройках меняется дата на значение текущей даты минус количество месяцев, которое указано в первом аргументе.

Далее программа ищет корневые папки клиентов, которые подходят под условия, указанные в настройках.

Далее в этих папках ищутся подпапки (не учитываются папки Dictionaries в корневых папках клиентов).

В каждой из этих папок ищутся файлы, дата изменения которых меньше даты в настройках (с учетом аргумента).

Найденные файлы группируются по годам и месяцам, потом архивируются по месяцам (названия файлов архивов - <год полностью>_<месяц цифрой с лидирующим нулем>(<месяц буквами полностью>), например "2020_09(Сентябрь).zip").

Файлы, добавленные в архив, удаляются.

Если в папке нет файлов для архивации, то папка ARCHIVE не создается.

<b>Описание файла settings.json</b>

pathToFtp - полный путь до папки, в которой ищутся корневые папки клиентов

dateBefore - дата, до которой архивируются файлы (если не указана, то будут архивироваться файлы, измененные до 01.01.0001, т.е. никакие)

foldersToArchive - список корневых папок клиентов, файлы в которых нужно архивировать (если не указано, то будут искаться все корневые папки по пути pathToFtp, в которых есть папки in или out или и та и другая)

exceptedFolders - список корневых папок клиентов, в которых не нужно архивировать файлы

<b>Особенности</b>

Если указать аргумент консоли целым числом, то дата в dateBefore не учитывается, и архивироваться будут файлы, дата изменения которых меньше даты, образованной значением текущей даты минус количество месяцев, которое указано в аргументе.

Например, сегодня 12/12/2031 (12 декабря 2031), в аргументе указано число 6, то будут архивироваться файлы, дата изменения которых меньше 12/06/2031 (12 июня 2031).
