# AI.SignalLabs

Библиотека цифровой обработки сигналов на языке C# 12, построенная на базе [AIFrameworkOpen](https://github.com/AIFrameworkOpen). Предоставляет инструменты для цифровой модуляции, демодуляции, формирования импульсов и автоматической регулировки усиления.

---

## Структура репозитория

```
AISignalLabs/
├── AI.SignalLabs/              # Основная библиотека (netstandard2.0, C# 12)
│   ├── AGC/                    # Автоматическая регулировка усиления (АРУ)
│   ├── Filters/                # Фильтры (SRRC и др.)
│   ├── Generators/             # Генераторы тестовых сигналов
│   └── Modulation/
│       ├── Demodulation/       # Квадратурная демодуляция
│       └── Modulation/
│           └── DigitalModulations/  # BPSK, QPSK, 8-QAM, 16-QAM
│
└── Demo/                       # Демонстрационные WinForms приложения
    ├── ModulationTest/         # Анализ цифровой модуляции и декодирования
    └── AGC/                    # Демонстрация алгоритмов АРУ
```

---

## Возможности библиотеки

### Цифровые модуляции (`Modulation/Modulation/DigitalModulations`)

Все типы модуляций наследуют базовый класс `BaseIQModulation`, предоставляющий:
- `MapBitsToSymbols(bool[] bits)` — маппинг битового потока в IQ-символы.
- `DemapSymbolsToBits(ComplexVector symbols, int expectedBits)` — декодирование методом максимального правдоподобия (Minimum Distance Decoding).

| Класс    | Бит/символ | Точек в созвездии | Нормировка      |
|----------|:----------:|:-----------------:|-----------------|
| `BPSK`   | 1          | 2                 | 1               |
| `QPSK`   | 2          | 4                 | 1/√2            |
| `QAM8`   | 3          | 8                 | смешанная       |
| `QAM16`  | 4          | 16                | 1/√10           |

### Квадратурная демодуляция (`Modulation/Demodulation`)

Класс `QudratureDemodulation` реализует IQ-разложение сигнала:

```
вход → ×cos(2πf₀t) → [SRRC или ФНЧ Баттерворта] → I
вход → ×sin(2πf₀t) → [SRRC или ФНЧ Баттерворта] → Q
```

**Два конструктора:**
- `QudratureDemodulation(f0, sr, cutoffFrequency)` — с фильтром Баттерворта.
- `QudratureDemodulation(f0, sr, symbolPeriod, rollOff, spanSymbols)` — с согласованным SRRC-фильтром (нулевая МСИ).

**Методы:**
- `GetIQComponents(signal)` — полный вектор IQ для всего сигнала.
- `GetIQSymbols(signal)` — только точки созвездия (центры символов).
- `GetIQBoth(signal, externalDelay)` — оба результата за один проход.

### Фильтр SRRC (`Filters`)

`RootRaisedCosineFilter` — корневой фильтр приподнятого косинуса (Square Root Raised Cosine).

Используется парами: на передатчике (формирование импульса) и приёмнике (согласованный фильтр). Пара SRRC × SRRC = RRC — нулевая межсимвольная интерференция (МСИ) в моменты выборки символов.

```csharp
var srrc = new RootRaisedCosineFilter(symbolPeriod: 1e-3, sampleRate: 44100, rollOff: 0.35, spanSymbols: 4);
Vector shaped = srrc.FilterOutp(impulses);
```

### Автоматическая регулировка усиления (`AGC`)

Интерфейс `IAGC` с потоковым методом `Calculate(double value)`.

| Класс             | Описание                                                                   |
|-------------------|----------------------------------------------------------------------------|
| `DirectAGC`       | Нормализация по RMS через пару IIR-фильтров (среднее и дисперсия)          |
| `LogAGC`          | Логарифмическое усреднение огибающей (лучше справляется с импульсными помехами) |
| `MinCombineAGC`   | Комбинация двух АРУ, выбирает минимальный по модулю выход в каждый момент  |

```csharp
IAGC agc = new MinCombineAGC();
double normalized = agc.Calculate(inputSample);
```

### Генераторы сигналов (`Generators`)

Потоковые генераторы с поддержкой аддитивного/мультипликативного шума и трендов:

| Класс            | Описание            |
|------------------|---------------------|
| `BaseGenerator`  | Базовый генератор   |
| `SineGenerator`  | Синусоидальный сигнал |
| `RectGenerator`  | Прямоугольный сигнал |

---

## Быстрый старт

### Полный цикл QPSK: модуляция → передача → демодуляция → декодирование

```csharp
using AI.SignalLab.Filters;
using AI.SignalLab.Modulation.Demodulation;
using AI.SignalLab.Modulation.Modulation.DigitalModulations;

const double Sr = 44100;        // частота дискретизации
const double F0 = 2000;         // несущая
const double T = 1e-3;          // длительность символа

// Передатчик
var modulation = new QPSK();
var txSrrcI = new RootRaisedCosineFilter(T, Sr, rollOff: 0.35, spanSymbols: 4);
var txSrrcQ = new RootRaisedCosineFilter(T, Sr, rollOff: 0.35, spanSymbols: 4);

bool[] bits = /* ваши данные */;
Complex[] symbols = modulation.MapBitsToSymbols(bits);

// Импульсы Дирака → SRRC → модуляция несущей
// ...

// Приёмник
var demod = new QudratureDemodulation(F0, Sr, T, rollOff: 0.35, spanSymbols: 4);
var (iqFull, iqSymbols) = demod.GetIQBoth(modulatedSignal, txDelay);

bool[] decoded = modulation.DemapSymbolsToBits(iqSymbols, bits.Length);
```

---

## Зависимости

| Зависимость      | Назначение                               |
|------------------|------------------------------------------|
| `AI`             | Статистика, базовые структуры данных     |
| `AI.DSP`         | IIR-фильтры, FIR-фильтры, DSP-утилиты   |
| `AIChartCV`      | Визуализация сигналов (только Demo)      |

Зависимости подключаются через ссылки на проекты из [AIFrameworkOpen](https://github.com/AIFrameworkOpen).

---

## Требования

- .NET SDK 8 или новее
- Visual Studio 2022 / Rider
- Клонированный репозиторий `AIFrameworkOpen` рядом в структуре `../C#/AIFrameworkOpen`

---

## Лицензия

[MIT](LICENSE)
