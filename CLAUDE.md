# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Purpose

This repository contains solutions for [Advent of Code](https://adventofcode.com/) programming challenges.

## Project Structure

Solutions should be organized by year and day:
```
/2024
  /Day01
  /Day02
  ...
/2025
  /Day01
  ...
```

## Advent of Code Conventions

- Each day has two parts (Part 1 and Part 2)
- Input files are unique per user - store them as `input.txt` in each day's folder
- Example inputs from problem descriptions can be stored as `example.txt` for testing
- Solutions should read from input files, not have input hardcoded

## Working with Solutions

When implementing a solution:
1. Read and understand the problem statement from adventofcode.com
2. Test with the example input first
3. Run against the actual input file
4. Part 2 often builds on Part 1 - keep Part 1 solution accessible
