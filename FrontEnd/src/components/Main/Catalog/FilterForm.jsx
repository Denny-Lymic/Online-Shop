import { HStack, Stack, Field, Input, Text, NumberInput, NativeSelect, Center } from "@chakra-ui/react";
import { Form, useLoaderData } from "react-router";
import PriceSlider from "./Slider";
import { useState } from "react";

export default function FilterForm({ filterState, setFilterState, sliderRangeStr, setSliderRangeStr }) {
    const { categories, maxPrice } = useLoaderData();

    return (
        <Stack>
            <Text fontSize="md" fontWeight="bold" mb={2}>Название</Text>
            <Field.Root mb={8}>
                <Input
                    name="search"
                    value={filterState.search}
                    onChange={e => setFilterState({ ...filterState, search: e.currentTarget.value })}
                />
            </Field.Root>
            <Text fontSize="md" fontWeight="bold" mb={2}>Категория</Text>
            <Field.Root mb={8}>
                <NativeSelect.Root>
                    <NativeSelect.Field
                        name="category"
                        value={filterState.category}
                        onChange={e => setFilterState({ ...filterState, category: e.target.value })}
                    >
                        {categories.map(category => (
                            <option value={category.category} key={category.id}>{category.category}</option>
                        ))}
                    </NativeSelect.Field>
                    <NativeSelect.Indicator />
                </NativeSelect.Root>
            </Field.Root>
            <Text fontSize="md" fontWeight="bold" mb={2}>Цена</Text>
            <HStack justify="space-between" align="start">
                <Field.Root>
                    <Field.Label>От</Field.Label>
                    <NumberInput.Root
                        name="minPrice"
                        value={filterState.range[0]}
                        onValueChange={({ value }) =>
                            setFilterState({ ...filterState, range: [value, filterState.range[1]] })
                        }
                        onBlur={() => {
                            if (Number(filterState.range[0]) > Number(filterState.range[1]))
                                setSliderRangeStr([filterState.range[1], filterState.range[1]]);
                            else
                                setSliderRangeStr(filterState.range);
                        }}
                        min={0}
                        max={filterState.range[1]}
                        clampValueOnBlur
                    >
                        <NumberInput.Control />
                        <NumberInput.Input />
                    </NumberInput.Root  >
                </Field.Root>
                <Field.Root>
                    <Field.Label>До</Field.Label>
                    <NumberInput.Root
                        name="maxPrice"
                        value={filterState.range[1]}
                        onValueChange={({ value }) => {
                            setFilterState({ ...filterState, range: [filterState.range[0], value] });
                        }}
                        onBlur={() => {
                            if (Number(filterState.range[1]) < Number(filterState.range[0]))
                                setSliderRangeStr([filterState.range[0], filterState.range[0]]);
                            else
                                setSliderRangeStr(filterState.range)
                        }}
                        min={filterState.range[0]}
                        max={maxPrice}
                        clampValueOnBlur
                    >

                        <NumberInput.Control />
                        <NumberInput.Input />
                    </NumberInput.Root>
                </Field.Root>
            </HStack>
            <PriceSlider
                value={sliderRangeStr}
                max={maxPrice}
                onChange={value => {
                    setFilterState({ ...filterState, range: value });
                    setSliderRangeStr(value);
                }}
            />
            <Text fontSize="lg" fontWeight="bold" my={8}>Сортировка</Text>
            <Field.Root orientation="horizontal" mb={2}>
                <Field.Label textAlign="center">По</Field.Label>
                <NativeSelect.Root>
                    <NativeSelect.Field
                        name="sortBy"
                        value={filterState.sortBy}
                        onChange={e => setFilterState({ ...filterState, sortBy: e.target.value })}
                    >
                        <option value="name">Имени</option>
                        <option value="price">Цене</option>
                    </NativeSelect.Field>
                    <NativeSelect.Indicator />
                </NativeSelect.Root>
            </Field.Root>
            <Field.Root orientation="horizontal">
                <Field.Label>Порядку</Field.Label>
                <NativeSelect.Root>
                    <NativeSelect.Field
                        name="sortOrder"
                        value={filterState.sortOrder}
                        onChange={e => setFilterState({ ...filterState, sortOrder: e.target.value })}
                    >
                        <option value="asc">Возрастанию</option>
                        <option value="desc">Убыванию</option>
                    </NativeSelect.Field>
                    <NativeSelect.Indicator />
                </NativeSelect.Root>
            </Field.Root>
        </Stack>
    )
}